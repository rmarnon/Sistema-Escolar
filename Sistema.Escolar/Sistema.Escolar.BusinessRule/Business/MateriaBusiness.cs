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
    public class MateriaBusiness : IMateria
    {
        private readonly ApplicationContext _context;
        Result<Materia> result = new();
        Materia materia = new();

        public MateriaBusiness(ApplicationContext context) => _context = context;

        public async Task<Result<Materia>> AlteracoesAsync(string nome, string novoNome, string descricao)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(new Materia
                {
                    Nome = novoNome,
                    Descricao = descricao,
                    Cadastro = DateTime.Now,
                    Status = Status.Ativo
                });

                if (!valido.IsValid)
                    return Retorno.NaoValidaMateria(valido);

                using (_context)
                {
                    materia = await _context.Materias.FirstOrDefaultAsync(x => x.Nome == nome);

                    if (materia is null)
                        return Retorno.NaoEncontradaMateria();

                    materia.Nome = novoNome;
                    materia.Descricao = descricao;

                    _context.SaveChanges();

                    return Retorno.Ok(materia);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        public async Task<Result<Materia>> AlteraStatusAsync(string nome, Status status)
        {
            try
            {
                using (_context)
                {
                    materia = await _context.Materias.FirstOrDefaultAsync(x => x.Nome == nome);

                    if (materia is null)
                        return Retorno.NaoEncontradaMateria();

                    var valido = Retorno.ValidaEntrada(new Materia
                    {
                        Nome = nome,
                        Descricao = materia.Descricao,
                        Cadastro = materia.Cadastro,
                        Status = status
                    });

                    if (!valido.IsValid)
                        return Retorno.NaoValidaMateria(valido);

                    materia.Status = status;

                    _context.SaveChanges();

                    return Retorno.Ok(materia);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        public async Task<Result<Materia>> CadastrarAsync(Materia materia)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(materia);

                if (!valido.IsValid)
                    return Retorno.NaoValidaMateria(valido);

                using (_context)
                {
                    foreach (var mat in await _context.Materias.ToListAsync())
                    {
                        if (mat.Nome == materia.Nome)
                        {
                            result.Error = true;
                            result.Message.Add("Matéria já está cadastrada");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    _context.Entry(materia).State = EntityState.Added;
                    _context.SaveChanges();

                    return Retorno.Ok(materia);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        public async Task<Result<Materia>> ExcluirAsync(string nome)
        {
            try
            {
                using (_context)
                {
                    materia = await _context.Materias.FirstOrDefaultAsync(x => x.Nome == nome);

                    if (materia is null)
                        return Retorno.NaoEncontradaMateria();

                    _context.Materias.Remove(materia);
                    _context.SaveChanges();

                    return Retorno.Ok(materia);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErros(e);
            }
        }

        private Result<Materia> RetornaErros(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
    }
}
