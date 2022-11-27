using FluentValidation;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;

namespace Sistema.Escolar.Validators.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            ValidateLogin();
            ValidateSenha();
            ValidateTipo();
        }

        private void ValidateTipo()
        {
            RuleFor(x => x.Tipo)
                .IsInEnum()
                .WithMessage("Tipo do usuário informado é inválido")
                .When(x => x.Tipo is TipoUsuario);
        }

        private void ValidateSenha()
        {
            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Senha deve ser informada!");

            RuleFor(x => x.Senha)
                .Length(8, 50)
                .WithMessage("Senha deve ter no mínimo 8 e no maximo 50 caracteres");
        }

        private void ValidateLogin()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login deve ser informado!");

            RuleFor(x => x.Login)
                .Length(3, 50)
                .WithMessage("Login deve ter no mínimo 3 e no  máximo 50 caracteres");
        }
    }
}
