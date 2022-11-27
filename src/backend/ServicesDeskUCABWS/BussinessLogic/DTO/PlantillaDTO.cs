using System.ComponentModel.DataAnnotations;

namespace ServicesDeskUCABWS.BussinessLogic.DTO
{

    public class PlantillaDTO
    {
        public int id { get; set; }

        public string? operacion { get; set; }

        public bool titulo { get; set; }

        public bool fecha { get; set; }

        public bool descripcion { get; set; }

        public bool asignadoa { get; set; }

        //Fk de la tabla Tickets
        public int TicketId { get; set; }

    }

    public class PlantillaDTOCreate
    {

        public string? operacion { get; set; }

        public bool titulo { get; set; }

        public bool fecha { get; set; }

        public bool descripcion { get; set; }

        public bool asignadoa { get; set; }

        //Fk de la tabla Tickets
        public int TicketId { get; set; }

    }

}