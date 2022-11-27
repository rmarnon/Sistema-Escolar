using FluentValidation;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Validators.Rules;

namespace Sistema.Escolar.Validators.Validators
{
    public class CursoValidator : AbstractValidator<Curso>
    {
        public CursoValidator()
        {
            ValidateNome();
            ValidateSituacao();
            ValidateCursoMateria();
        }

        private void ValidateCursoMateria()
        {
            RuleForEach(x => x.CursoMaterias)
                .NotEmpty()
                .WithMessage("Informe uma disciplina para o curso");

            RuleForEach(x => x.CursoMaterias)
                .ChildRules(y =>
                {
                    y.RuleFor(x => x.Materia.Status)
                    .Must(x => x.Equals(Status.Ativo))
                    .WithMessage("Matéria deve estar com status 'Ativo'");
                });
        }

        private void ValidateSituacao()
        {
            RuleFor(x => x.Situacao)
                .NotEmpty()
                .WithMessage("Situação do curso não foi informado");

            RuleFor(x => x.Situacao)
                .Must(x => x.Equals(Status.Ativo))
                .WithMessage("Cadastro permitido apenas para cursos com status 'Ativa'");
        }

        private void ValidateNome()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Curso deve ser informado");

            RuleFor(x => x.Nome)
                .Must(CharsValidate.ValidaTexto)
                .When(x => !string.IsNullOrWhiteSpace(x.Nome))
                .WithMessage("Campo 'Curso' aceita apenas letras");

            RuleFor(x => x.Nome)
                .Length(3, 50)
                .WithMessage("Campo 'Curso' deve ter no mínimo 3 e no máximo 50 letras");
        }
    }
}
