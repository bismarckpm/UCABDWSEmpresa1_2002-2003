namespace ServicesDeskUCABWS.Exceptions
{
    public class PrioridadException : Exception
    {
        public PrioridadException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            logger.LogError(message, innerException);
        }
    }
}