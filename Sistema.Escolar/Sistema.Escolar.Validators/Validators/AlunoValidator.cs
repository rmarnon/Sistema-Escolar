using FluentValidation;
using Sistema.Escolar.Models;
using Sistema.Escolar.Models.Enums;
using Sistema.Escolar.Validators.Rules;

namespace Sistema.Escolar.Validators.Validators
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        public AlunoValidator()
        {
            ValidateNome();
            ValidateSobrenome();
            ValidateNascimento();
            ValidateCpf();
            ValidateAlunoCurso();
            ValidateAlunoMateria();
        }

        private void ValidateAlunoMateria()
        {
            RuleForEach(x => x.AlunoMaterias)
                .ChildRules(y =>
                {
                    y.RuleFor(x => x.Nota)
                    .Must(x => x >= 0 && x <= 100)
                    .WithMessage("Campo 'Nota' deve estar no intervalo [0-100]");
                });
        }

        private void ValidateAlunoCurso()
        {
            RuleForEach(x => x.AlunoCursos)
                .NotEmpty()
                .WithMessage("Informe um curso para realizar matrícula");

            RuleForEach(x => x.AlunoCursos)
                .ChildRules(y =>
                {
                    y.RuleFor(x => x.Curso.Situacao)
                    .Must(x => x.Equals(Status.Ativo))
                    .WithMessage("Só é possível realizar matrícula em curso com status 'Ativo'");
                });
        }

        private void ValidateCpf()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage("Campo 'CPF' deve ser informado");

            RuleFor(x => x.Cpf)
                .Must(CPFValidate.Validate)
                .WithMessage("CPF Inválido!");
        }

        private void ValidateNascimento()
        {
            RuleFor(x => x.Nascimento)
                .NotEmpty()
                .WithMessage("Campo 'Data' deve ser informado");

            RuleFor(x => x.Nascimento)
                .Must(ValidaIdade)
                .WithMessage("Aluno deve possuir 18 anos ou mais");
        }

        private void ValidateSobrenome()
        {
            RuleFor(x => x.Sobrenome)
                .NotEmpty()
                .WithMessage("Sobrenome deve ser informado");

            RuleFor(x => x.Sobrenome)
                .MaximumLength(20)
                .WithMessage("Campo 'Sobrenome' deve ter no máximo 20 caracteres");
        }

        private void ValidateNome()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome deve ser informado");

            RuleFor(x => x.Nome)
                .Must(CharsValidate.ValidaTexto)
                .When(x => !string.IsNullOrWhiteSpace(x.Nome))
                .WithMessage("Campo 'Nome' aceita somente letras");

            RuleFor(x => x.Nome)
                .Length(3, 20)
                .WithMessage("Campo 'Nome' deve ter no mínimo 3 e no máximo 20 letras");
        }

        private static bool ValidaIdade(DateTime nascimento)
        {
            var today = DateTime.Today;
            int idade = today.Year - nascimento.Year;
            if (today < nascimento.AddYears(idade)) idade--;
            return idade >= 18;
        }
    }
}
