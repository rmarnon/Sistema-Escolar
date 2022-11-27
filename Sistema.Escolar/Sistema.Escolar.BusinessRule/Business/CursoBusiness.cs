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
    public class CursoBusiness : ICurso
    {
        private readonly ApplicationContext _context;

        public CursoBusiness(ApplicationContext context) => this._context = context;
        Result<Curso> result = new();
        Curso curso = new();
        Materia materia = new();

        public async Task<Result<Curso>> CadastraAsync(Curso curso)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(curso);

                if (!valido.IsValid)
                    return Retorno.NaoValidaCurso(valido);

                using (_context)
                {
                    foreach (var course in await _context.Cursos.ToListAsync())
                    {
                        if (course.Nome == curso.Nome)
                        {
                            result.Error = true;
                            result.Message.Add("Curso já está cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    _context.Entry(curso).State = EntityState.Added;
                    _context.SaveChanges();

                    return Retorno.Ok(curso);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        public async Task<Result<Curso>> AlteraSituacaoAsync(string nome, Status situacao)
        {
            try
            {
                using (_context)
                {
                    curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Nome == nome);

                    if (curso is null)
                        return Retorno.NaoEncontradoCurso();

                    curso.Situacao = situacao;
                    _context.SaveChanges();

                    return Retorno.Ok(curso);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        public async Task<Result<Curso>> VinculaMateriaAsync(string nomeCurso, string nomeMateria)
        {
            try
            {
                using (_context)
                {
                    curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Nome == nomeCurso);

                    if (curso is null)
                        return Retorno.NaoEncontradoCurso();

                    var validaCurso = Retorno.ValidaEntrada(curso);

                    if (!validaCurso.IsValid)
                        return Retorno.NaoValidaCurso(validaCurso);

                    materia = await _context.Materias.FirstOrDefaultAsync(y => y.Nome == nomeMateria);

                    if (materia is null)
                    {
                        result.Error = true;
                        result.Message.Add("Materia não cadastrada");
                        result.Status = HttpStatusCode.NotFound;
                        return result;
                    }

                    var validaMateria = Retorno.ValidaEntrada(materia);

                    if (!validaMateria.IsValid)
                        return Retorno.NaoValidaCurso(validaMateria);

                    if (materia.Status != Status.Ativo)
                    {
                        result.Error = true;
                        result.Message.Add("Curso só permite cadastro de matérias com status 'Ativo'");
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    var cursoMateria = new CursoMateria { Curso = curso, Materia = materia };

                    _context.Entry(cursoMateria).State = EntityState.Added;
                    _context.SaveChanges();

                    return Retorno.Ok(curso);

                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        public async Task<Result<Curso>> ExcluirAsync(string nome)
        {
            try
            {
                using (_context)
                {
                    curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Nome == nome);

                    if (curso is null)
                        return Retorno.NaoEncontradoCurso();

                    _context.Cursos.Remove(curso);
                    _context.SaveChanges();

                    return Retorno.Ok(curso);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        private Result<Curso> RetornaErro(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
    }
}
