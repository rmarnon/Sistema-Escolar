using FluentValidation.Results;
using Sistema.Escolar.Models;
using Sistema.Escolar.Validators.Validators;
using System.Net;

namespace Sistema.Escolar.Util
{
    public static class Retorno
    {
        public static ValidationResult ValidaEntrada(Usuario usuario) => new UsuarioValidator().Validate(usuario);
        public static ValidationResult ValidaEntrada(Materia materia) => new MateriaValidator().Validate(materia);
        public static ValidationResult ValidaEntrada(Aluno aluno) => new AlunoValidator().Validate(aluno);
        public static ValidationResult ValidaEntrada(Curso curso) => new CursoValidator().Validate(curso);

        public static Result<Aluno> Ok(Aluno aluno)
        {
            var result = new Result<Aluno>
            {
                Data = aluno,
                Error = false,
                Status = HttpStatusCode.OK
            };
            result.Message.Add("Ok");
            return result;
        }

        public static Result<Curso> Ok(Curso curso)
        {
            var result = new Result<Curso>
            {
                Data = curso,
                Error = false,
                Status = HttpStatusCode.OK
            };
            result.Message.Add("Ok");
            return result;
        }

        public static Result<Materia> Ok(Materia materia)
        {
            var result = new Result<Materia>
            {
                Data = materia,
                Error = false,
                Status = HttpStatusCode.OK
            };
            result.Message.Add("Ok");
            return result;
        }

        public static Result<Usuario> Ok(Usuario user)
        {
            var result = new Result<Usuario>
            {
                Data = user,
                Error = false,
                Status = HttpStatusCode.OK
            };
            result.Message.Add("Ok");
            return result;
        }

        public static Result<Aluno> NaoEncontradoAluno()
        {
            var result = new Result<Aluno>
            {
                Error = true,
                Status = HttpStatusCode.NotFound
            };
            result.Message.Add("Aluno não está matriculado");
            return result;
        }

        public static Result<Curso> NaoEncontradoCurso()
        {
            var result = new Result<Curso>
            {
                Error = true,
                Status = HttpStatusCode.NotFound
            };
            result.Message.Add("Curso não está cadastrado");
            return result;
        }

        public static Result<Materia> NaoEncontradaMateria()
        {
            var result = new Result<Materia>
            {
                Error = true,
                Status = HttpStatusCode.NotFound
            };
            result.Message.Add("Matéria não está cadastrada");
            return result;
        }

        public static Result<Usuario> NaoEncontradoUsuario()
        {
            var result = new Result<Usuario>
            {
                Error = true,
                Status = HttpStatusCode.NotFound
            };
            result.Message.Add("Usuário não encontrado");
            return result;
        }

        public static Result<Aluno> NaoValidaAluno(ValidationResult valido)
        {
            var result = new Result<Aluno>
            {
                Error = true,
                Status = HttpStatusCode.BadRequest
            };
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            return result;
        }

        public static Result<Curso> NaoValidaCurso(ValidationResult valido)
        {
            var result = new Result<Curso>
            {
                Error = true,
                Status = HttpStatusCode.BadRequest
            };
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            return result;
        }

        public static Result<Materia> NãoValidaMateria(ValidationResult valido)
        {
            var result = new Result<Materia>
            {
                Error = true,
                Status = HttpStatusCode.BadRequest
            };
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            return result;
        }

        public static Result<Usuario> NaoValidaUsuario(ValidationResult valido)
        {
            var result = new Result<Usuario>
            {
                Error = true,
                Status = HttpStatusCode.BadRequest
            };
            result.Message.AddRange(valido.Errors.Select(x => x.ErrorMessage));
            return result;
        }

        public static Result<Usuario> SenhaInvalida()
        {
            var result = new Result<Usuario>
            {
                Error = true,
                Status = HttpStatusCode.BadRequest
            };
            result.Message.Add("Senha inválida");
            return result;
        }
    }
}
