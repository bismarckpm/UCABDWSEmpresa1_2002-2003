


namespace ServicesDeskUCABWS.Exceptions
{
    public class EtiquetaException : Exception
    {
        public EtiquetaException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            logger.LogError(message, innerException);
        }
    }
}