using FluentValidation;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Validators.Rules;

namespace Sistema.Escolar.Validators.Validators
{
    public class MateriaValidator : AbstractValidator<Materia>
    {
        public MateriaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome deve ser informado")
                .MaximumLength(50)
                .WithMessage("Campo 'Nome' permite no máximo 50 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descrição deve ser informada")
                .Must(CharsValidate.Validate)
                .WithMessage("Campo 'Descrição' aceita apenas letras");

            RuleFor(x => x.Cadastro)
                .NotEmpty()
                .WithMessage("Data de cadastro deve ser informado")
                .Must(x => x.Date < DateTime.Now)
                .WithMessage("Data de cadastro não pode ser datas futuras");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status informado não é válido")
                .NotEmpty()
                .WithMessage("Status da matéria deve ser informado")
                .Must(x => x.Equals(Status.Ativo) || x.Equals(Status.Inativo))
                .WithMessage("Status permitidos [Ativo|Inativo]");
        }
    }
}
