namespace ServicesDeskUCABWS.Exceptions
{
    public class DepartamentoException : Exception
    {
        public DepartamentoException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            logger.LogError(message, innerException);
        }
    }
}