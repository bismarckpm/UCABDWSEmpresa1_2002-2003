namespace ServicesDeskUCABWS.Exceptions
{
    public class TickectExeception : Exception 
    {
         public string Mensaje { get; set; }

        public Exception Excepcion { get; set; }

        public string CodigoError { get; set; }

        public string MensajeSoporte { get; set; }

        
        public TickectExeception(string _mensaje, Exception _excepcion, string _mensajesoporte, string _codigoError)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
            CodigoError = _codigoError;
            MensajeSoporte = _mensajesoporte;
        }

       
        public TickectExeception(string _mensaje, string _mensajeSoporte, Exception _excepcion)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
            MensajeSoporte = _mensajeSoporte;
        }

    
        public TickectExeception(string _mensaje, Exception _excepcion)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
        }

    
        public TickectExeception(string _mensaje)
        {
            Mensaje = _mensaje;
        }
    }
}