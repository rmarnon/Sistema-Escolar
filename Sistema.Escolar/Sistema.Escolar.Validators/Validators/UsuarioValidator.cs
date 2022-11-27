using FluentValidation;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;

namespace Sistema.Escolar.Validators.Validators
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login deve ser informado!")
                .Length(3, 50)
                .WithMessage("Login deve ter no mínimo 3 e no  máximo 50 caracteres");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Senha deve ser informada!")
                .Length(8, 50)
                .WithMessage("Senha deve ter no mínimo 8 e no maximo 50 caracteres");

            RuleFor(x => x.Tipo)
                .IsInEnum()
                .WithMessage("Tipo do usuário informado é inválido")
                .When(x => x.Tipo is TipoUsuario);
        }
    }
}
