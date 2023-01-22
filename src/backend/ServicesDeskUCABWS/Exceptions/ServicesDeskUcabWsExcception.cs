namespace ServicesDeskUCABWS.Exceptions
{
    public class ServicesDeskUcabWsException : Exception
    {
        public string Mensaje { get; set; }

        public Exception Excepcion { get; set; }

        public string CodigoError { get; set; }

        public string MensajeSoporte { get; set; }

        public ServicesDeskUcabWsException(string mensaje, Exception exception, string mensajeSoporte, string codigoError)
        {
            Mensaje = mensaje;
            Excepcion = exception;
            MensajeSoporte = mensajeSoporte;
            CodigoError = codigoError;
        }

        public ServicesDeskUcabWsException(string mensaje, string mensajeSoporte, Exception exception)
        {
            Mensaje = mensaje;
            MensajeSoporte = mensajeSoporte;
            Excepcion = exception;
        }

        public ServicesDeskUcabWsException(string mensaje, Exception exception)
        {
            Mensaje = mensaje;
            Excepcion = exception;
        }

        public ServicesDeskUcabWsException(string mensaje)
        {
            Mensaje = mensaje;
        }

        public ServicesDeskUcabWsException()
        {
            
        }
    }
}