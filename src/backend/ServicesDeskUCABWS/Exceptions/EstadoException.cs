


namespace ServicesDeskUCABWS.Exceptions
{
    public class EstadoException : Exception
    {
        public EstadoException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            logger.LogError(message, innerException);
        }


    }
}