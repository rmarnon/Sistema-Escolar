namespace Sistema.Escolar.BusinessRule.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public BusinessException(string message)
            : base(message) { }
    }
}
