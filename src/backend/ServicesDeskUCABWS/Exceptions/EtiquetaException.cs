


namespace ServicesDeskUCABWS.Exceptions
{
    public class EtiquetaException : Exception
    {
        public EtiquetaException(string message, Exception innerException, ILogger logger) : base(message, innerException)
        {
            logger.LogError(message, innerException);
        }

        public EtiquetaException() : base()
        {

        }
        public EtiquetaException(string message) : base(message)
        {
            Console.WriteLine(message);
        }

        public EtiquetaException(string message, Exception innerException) : base(message, innerException)
        {
            Console.WriteLine(message);
            Console.WriteLine(innerException);
        }
    }
}