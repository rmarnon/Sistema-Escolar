using System.Text.RegularExpressions;

namespace Sistema.Escolar.Validators.Rules
{
    public static class CharsValidate
    {
        public static bool ValidaTexto(string letras) => new Regex("^[A-Za-z\\s]{1,}$").IsMatch(letras);
    }
}
