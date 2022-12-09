using Microsoft.EntityFrameworkCore;
using Sistema.Escolar.BusinessRule.Exceptions;
using Sistema.Escolar.BusinessRule.Interfaces;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Repositories.Data;
using Sistema.Escolar.Util;
using System.Net;

namespace Sistema.Escolar.BusinessRule.Business
{
    public class UsuarioBusiness : IUsuario
    {
        private readonly ApplicationContext _context;
        Result<Usuario> result = new();
        Usuario user = new();

        public UsuarioBusiness(ApplicationContext context) => this._context = context;

        public async Task<Result<Usuario>> AlterarLoginAsync(string login, string novoLogin, string senha)
        {
            try
            {
                using (_context)
                {
                    var valido = Retorno.ValidaEntrada(new Usuario { Login = novoLogin, Senha = senha, Tipo = TipoUsuario.Aluno });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    user.Login = novoLogin;
                    _context.SaveChanges();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        public async Task<Result<Usuario>> AlteraSenhaAsync(string login, string senha, string novaSenha)
        {
            try
            {
                using (_context)
                {
                    user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    var valido = Retorno.ValidaEntrada(new Usuario { Login = login, Senha = novaSenha, Tipo = user.Tipo });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    user.Senha = novaSenha;
                    _context.Usuarios.Update(user);
                    _context.SaveChanges();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        public async Task<Result<Usuario>> CadastrarAsync(Usuario user)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(user);

                if (!valido.IsValid)
                    return Retorno.NaoValidaUsuario(valido);

                using (_context)
                {
                    foreach (var usuario in _context.Usuarios)
                    {
                        if (usuario.Login == user.Login)
                        {
                            result.Error = true;
                            result.Message.Add("Usuário já está cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    await _context.Usuarios.AddAsync(user);
                    _context.SaveChanges();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        public async Task<Result<Usuario>> ConectarAsync(string login, string senha)
        {
            try
            {
                using (_context)
                {
                    user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    var valido = Retorno.ValidaEntrada(new Usuario { Login = login, Senha = senha, Tipo = user.Tipo });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        public async Task<Result<Usuario>> ExcluirAsync(string login, string senha)
        {
            try
            {
                using (_context)
                {
                    user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    var valido = Retorno.ValidaEntrada(new Usuario { Login = login, Senha = senha, Tipo = user.Tipo });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    _context.Usuarios.Remove(user);
                    _context.SaveChanges();

                    return Retorno.Ok(user);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        private Result<Usuario> RetornaErros(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
    }
}
