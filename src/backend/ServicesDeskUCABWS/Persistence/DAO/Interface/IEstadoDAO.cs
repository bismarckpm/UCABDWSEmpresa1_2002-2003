using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IEstadoDAO
    {
        public Task<List<EstadoResponseDTO>> GetEstadosDAO();
        public Task<EstadoResponseDTO> GetEstadoDAO(int id);
        public Task<EstadoDTO> AgregarEstadoDAO(Estado Estado);
        public Task<EstadoDTO> ActualizarEstadoDAO(Estado Estado, int id);
        public Task<Boolean> EliminarEstadoDAO(int id);



    }
}