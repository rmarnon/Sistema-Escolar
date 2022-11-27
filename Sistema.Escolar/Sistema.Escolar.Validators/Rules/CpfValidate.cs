using System.Text.RegularExpressions;

namespace Sistema.Escolar.Validators.Rules
{
    public static class CPFValidate
    {
        public static bool Validate(string cpf)
        {
            if (cpf is null) return false;

            int soma, resto;
            string tempCpf, digito;

            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var match = Regex.IsMatch(cpf, @"^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$");

            if (match)
            {
                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                tempCpf = cpf[..9];
                soma = 0;

                resto = CalculaResto(multiplicador1, tempCpf, ref soma);

                digito = resto.ToString();
                tempCpf += digito;
                soma = 0;

                resto = CalculaResto(multiplicador2, tempCpf, ref soma);

                digito += resto.ToString();

                return cpf.EndsWith(digito);
            }

            return false;
        }

        private static int CalculaResto(int[] multiplicador, string tempCpf, ref int soma)
        {
            for (var i = 0; i < multiplicador.Length; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador[i];

            var resto = soma % 11;
            return (resto < 2) ? 0 : 11 - resto;
        }
    }
}
