


namespace ServicesDeskUCABWS.Exceptions
{
    public class PlantillaException : Exception
    {
        public override string Message { get; }
        public Exception innerException { get; set; }
        public PlantillaException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            this.Message = message;
            this.innerException = innerException;
            logger.LogError(message, innerException);
        }

    }
}