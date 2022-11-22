


namespace ServicesDeskUCABWS.Exceptions
{
    public class PlantillaException : Exception
    {
        public PlantillaException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            logger.LogError(message, innerException);
        }

        public PlantillaException() : base()
        {

        }
        public PlantillaException(string message) : base(message)
        {
            Console.WriteLine(message);
        }

        public PlantillaException(string message, Exception innerException) : base(message, innerException)
        {
            Console.WriteLine(message);
            Console.WriteLine(innerException);
        }
    }
}