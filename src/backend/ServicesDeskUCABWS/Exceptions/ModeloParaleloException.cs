namespace ServicesDeskUCABWS.Exceptions
{
    public class ModeloParaleloException : Exception
    {
        public ModeloParaleloException(string message, Exception innerException) : base(message, innerException)
        {
            Console.WriteLine(message, innerException);
        }
    }
}