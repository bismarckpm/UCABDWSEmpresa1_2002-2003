namespace ServicesDeskUCABWS.BussinessLogic.DTO
{
    public class TicketCDTO
    {
        public int? id {get;set;}
        public int? idasignad {get;set;}
        public int? idestado {get;set;}
        public int? idprioridad {get;set;}
        public int? idcategoria {get;set;}
        public string? nombre { get; set; }
        public DateTime? fecha { get; set; }
        public string? descripcion { get; set; }
        public string? creadopor {get; set;}
        public string? asginadoa { get; set; }
        public string? prioridad { get; set; }

        public string? estado {get;set;}
        public string? categoria {get;set;}
        public string? departamento {get;set;}
        public int? departamentoid{get;set;}
    }
}