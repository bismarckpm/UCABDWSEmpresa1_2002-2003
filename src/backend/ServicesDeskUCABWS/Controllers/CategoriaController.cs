using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.Persistence.Entity;
using Microsoft.AspNetCore.Mvc;
using ServicesDeskUCABWS.Persistence.DAO.Interface;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;


namespace ServicesDeskUCABWS.Controllers
{
    [ApiController]
    [Route("Categoria")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaDAO _dao;
        private readonly ILogger<CategoriaController> _log;

        public CategoriaController(ICategoriaDAO dao, ILogger<CategoriaController> logger)
        {
            _log = logger;
            _dao = dao;
        }

        [HttpPost]
        [Route("CreateCategoria/")]
        public CategoriaDTO CreateCategoria([FromBody] CategoriaDTO dto1)
        {
            try
            {               
                var data =  _dao.AgregarCategoriaDAO(CategoriaMapper.DtoToEntity(dto1));
                 return data;   

            }catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("ConsultaCategorias/")]
        public List<CategoriaDTO> ConsultaCategorias()
        {
            try
            {
                var data = _dao.ConsultarTodosCategoriasDAO();
                return data;

            }catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpGet]
        [Route("ConsultaCategoria/{id}")]
        public CategoriaDTO ConsultaCategoria([Required][FromRoute] int id)
        {
            try
            {
                var data = _dao.ConsultaCategoriaDAO(id);
                return data;

            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                throw ex.InnerException!;
            }
        }

        [HttpPut]
        [Route("Actualizar/")]
        public CategoriaDTO ActualizarCategoria([Required][FromBody] CategoriaDTO dto)
        {
            try
            {
                return _dao.ActualizarCategoriaDAO(CategoriaMapper.DtoToEntity(dto));

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public CategoriaDTO EliminarCategoria([Required][FromRoute] int id)
        {
            try
            {

                return _dao.EliminarCategoriaDAO(id);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw ex.InnerException!;    
            }
        }
    }

}