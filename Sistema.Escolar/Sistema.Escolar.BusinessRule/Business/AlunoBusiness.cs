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
    public class AlunoBusiness : IAluno
    {
        private readonly ApplicationContext _context;

        public AlunoBusiness(ApplicationContext context) => this._context = context;
        Result<Aluno> result = new();
        Aluno aluno = new();
        Curso curso = new();
        Materia materia = new();

        public async Task<Result<Aluno>> MatricularAsync(Aluno aluno, string nomeCurso)
        {
            try
            {
                var valido = Retorno.ValidaEntrada(aluno);

                if (!valido.IsValid)
                    return Retorno.NaoValidaAluno(valido);

                using (_context)
                {
                    foreach (var student in _context.Alunos)
                    {
                        if (student.Cpf == aluno.Cpf)
                        {
                            result.Error = true;
                            result.Message.Add("Aluno já está cadastrado");
                            result.Status = HttpStatusCode.BadRequest;
                            return result;
                        }
                    }

                    curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Nome == nomeCurso);

                    if (curso is null)
                    {
                        result.Error = true;
                        result.Message.Add("Curso não está sendo ofertado");
                        result.Status = HttpStatusCode.NotFound;
                        return result;
                    }

                    if (curso.Situacao != Status.Ativo)
                    {
                        result.Error = true;
                        result.Message.Add("A matrícula só é permitida para cursos com status 'Ativo'");
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    var alunoCurso = new AlunoCurso { Aluno = aluno, Curso = curso };

                    _context.Entry(aluno).State = EntityState.Added;
                    _context.Entry(alunoCurso).State = EntityState.Added;
                    _context.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        public async Task<Result<Aluno>> InserirNotaAsync(string cpf, string nomeMateria, double nota)
        {
            try
            {
                using (_context)
                {
                    materia = await _context.Materias.FirstOrDefaultAsync(x => x.Nome == nomeMateria);

                    if (materia is null)
                    {
                        result.Error = true;
                        result.Message.Add("Matéria não cadastrada");
                        result.Status = HttpStatusCode.NotFound;
                        return result;
                    }

                    var valido = Retorno.ValidaEntrada(materia);

                    if (!valido.IsValid)
                    {
                        result.Error = true;
                        result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    if (materia.Status != Status.Ativo)
                    {
                        result.Error = true;
                        result.Message.Add("Materia deve estar com status 'Ativo' para receber notas");
                        result.Status = HttpStatusCode.BadRequest;
                        return result;
                    }

                    aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Cpf == cpf);

                    if (aluno is null)
                        return Retorno.NaoEncontradoAluno();

                    aluno.AlunoMaterias.Add(
                        new AlunoMateria
                        {
                            Aluno = aluno,
                            Materia = materia,
                            Nota = nota
                        });

                    valido = Retorno.ValidaEntrada(aluno);

                    if (!valido.IsValid)
                        return Retorno.NaoValidaAluno(valido);

                    _context.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        public async Task<Result<Aluno>> AlterarNomeAsync(string cpf, string novoNome)
        {
            try
            {
                using (_context)
                {
                    aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Cpf == cpf);

                    if (aluno is null)
                        return Retorno.NaoEncontradoAluno();

                    aluno.Nome = novoNome;

                    var valido = Retorno.ValidaEntrada(aluno);

                    if (!valido.IsValid)
                        return Retorno.NaoValidaAluno(valido);

                    _context.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        public async Task<Result<Aluno>> AlterarSobrenomeAsync(string cpf, string novoSobrenome)
        {
            try
            {
                using (_context)
                {
                    aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Cpf == cpf);

                    if (aluno is null)
                        return Retorno.NaoEncontradoAluno();

                    aluno.Sobrenome = novoSobrenome;

                    var valido = Retorno.ValidaEntrada(aluno);

                    if (!valido.IsValid)
                        return Retorno.NaoValidaAluno(valido);

                    _context.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        public async Task<Result<Aluno>> ExcluirAsync(string cpf)
        {
            try
            {
                using (_context)
                {
                    aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Cpf == cpf);

                    if (aluno is null)
                        return Retorno.NaoEncontradoAluno();

                    _context.Alunos.Remove(aluno);
                    _context.SaveChanges();

                    return Retorno.Ok(aluno);
                }
            }
            catch (BusinessException e)
            {
                return RetornaErro(e);
            }
        }

        private Result<Aluno> RetornaErro(BusinessException e)
        {
            result.Error = true;
            result.Message.Add(e.Message);
            result.Status = HttpStatusCode.InternalServerError;
            return result;
        }
    }
}
