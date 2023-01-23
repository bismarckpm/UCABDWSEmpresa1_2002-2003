


namespace ServicesDeskUCABWS.Exceptions
{
    public class EstadoException : Exception
    {
        public override string Message { get; }
        public Exception innerException { get; set; }
        public EstadoException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            this.Message = message;
            this.innerException = innerException;
            logger.LogError(message, innerException);
        }


    }
}