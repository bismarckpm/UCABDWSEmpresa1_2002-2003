


namespace ServicesDeskUCABWS.Exceptions
{
    public class PlantillaException : Exception
    {
        public PlantillaException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            logger.LogError(message, innerException);
        }

    }
}