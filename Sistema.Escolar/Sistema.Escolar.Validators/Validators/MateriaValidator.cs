using FluentValidation;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;

namespace Sistema.Escolar.Validators.Validators
{
    public class MateriaValidator : AbstractValidator<Materia>
    {
        public MateriaValidator()
        {
            ValidateNome();
            ValidateDescricao();
            ValidateCadastro();
            ValidateStatus();
        }

        private void ValidateStatus()
        {
            RuleFor(x => x.Status)
               .NotEmpty()
               .WithMessage("Status da matéria deve ser informado");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status informado não é válido");

            RuleFor(x => x.Status)
                .Must(x => x.Equals(Status.Ativo) || x.Equals(Status.Inativo))
                .WithMessage("Status permitidos [Ativo|Inativo]");
        }

        private void ValidateCadastro()
        {
            RuleFor(x => x.Cadastro)
                .NotEmpty()
                .WithMessage("Data de cadastro deve ser informado");

            RuleFor(x => x.Cadastro)
                .Must(x => x.Date < DateTime.Now)
                .WithMessage("Data de cadastro não pode ser datas futuras");
        }

        private void ValidateDescricao()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Descrição deve ser informada");
        }

        private void ValidateNome()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome deve ser informado");

            RuleFor(x => x.Nome)
                .MaximumLength(50)
                .WithMessage("Campo 'Nome' permite no máximo 50 caracteres");
        }
    }
}
