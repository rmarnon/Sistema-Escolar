namespace Sistema.Escolar.Validators.Rules
{
    public static class CharsValidate
    {
        public static bool Validate(string letras)
        {
            var validar = false;

            if (letras is null) return validar;

            foreach (var letra in letras)
            {
                validar = !char.IsDigit(letra);

                if (!validar) break;
            }
            return validar;
        }
    }
}
