﻿using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationContext context;
        Result<Usuario> result = new Result<Usuario>();
        Usuario user = new Usuario();

        public UsuarioBusiness(ApplicationContext context) => this.context = context;

        public async Task<Result<Usuario>> AlterarLoginAsync(string login, string novoLogin, string senha)
        {
            try
            {
                using (context)
                {
                    var valido = Retorno.ValidaEntrada(new Usuario { Login = novoLogin, Senha = senha, Tipo = TipoUsuario.Aluno });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    user = await context.Usuarios.FirstOrDefaultAsync(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    user.Login = novoLogin;
                    context.SaveChanges();

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
                using (context)
                {
                    var valido = Retorno.ValidaEntrada(new Usuario { Login = login, Senha = novaSenha });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    user = await context.Usuarios.FirstOrDefaultAsync(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    user.Senha = novaSenha;
                    context.SaveChanges();

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

                using (context)
                {
                    foreach (var usuario in context.Usuarios)
                    {
                        if (usuario.Login == user.Login)
                        {
                            result.Error = true;
                            result.Message.Add("Usuário já está cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    await context.Usuarios.AddAsync(user);
                    context.SaveChanges();

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
                using (context)
                {
                    user = await context.Usuarios.FirstOrDefaultAsync(x => x.Login == login);

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
                using (context)
                {
                    user = await context.Usuarios.FirstOrDefaultAsync(x => x.Login == login);

                    if (user is null)
                        return Retorno.NaoEncontradoUsuario();

                    var valido = Retorno.ValidaEntrada(new Usuario { Login = login, Senha = senha, Tipo = user.Tipo });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaUsuario(valido);

                    if (user.Senha != senha)
                        return Retorno.SenhaInvalida();

                    context.Usuarios.Remove(user);
                    context.SaveChanges();

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
