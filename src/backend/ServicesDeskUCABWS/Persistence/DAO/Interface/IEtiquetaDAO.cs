using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface IEtiquetaDAO
    {
        public Task<List<Etiqueta>> ConsultarEtiquetasDAO();

        public Task<EtiquetaDTO> AgregarEtiquetaDAO(Etiqueta etiqueta);

        public Task<Etiqueta> ObtenerEtiquetaDAO(int id);
        public Task<Etiqueta> ActualizarEtiquetaDAO(Etiqueta etiqueta, int id);

        public Task<Boolean> EliminarEtiquetaDAO(int id);
    }
}