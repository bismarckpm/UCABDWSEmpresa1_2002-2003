


namespace ServicesDeskUCABWS.Exceptions
{
    public class EstadoException : Exception
    {
        public EstadoException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            logger.LogError(message, innerException);
        }

        public EstadoException() : base()
        {

        }
        public EstadoException(string message) : base(message)
        {
            Console.WriteLine(message);
        }

        public EstadoException(string message, Exception innerException) : base(message, innerException)
        {
            Console.WriteLine(message);
            Console.WriteLine(innerException);
        }
    }
}