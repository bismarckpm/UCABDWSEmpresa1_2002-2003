using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations;

public class ModeloParaleloDAO : IModeloParaleloDAO
{
    private readonly IMigrationDbContext _context;
    public ModeloParaleloDAO(IMigrationDbContext context)
    {
        this._context = context;
    }
    public ModeloParaleloDTO AgregarModeloParaleloDAO(ModeloParalelo modeloParalelo)
    {
        try
        {
            _context.ModeloParalelos.Add(modeloParalelo);
            _context.DbContext.SaveChanges();

            var data = _context.ModeloParalelos.Where(a => a.paraleloId == modeloParalelo.paraleloId)
            .Select(
                    a => new ModeloParaleloDTO
                    {
                        id = a.paraleloId,
                        nombre = a.nombre,
                        cantidadAprobaciones = a.cantidadAprobaciones,
                        categoria = CategoriaMapper.EntityToDto(a.categoria)
                    }
                );

                return data.First();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Transaccion Fallida", ex);
            }
        }

         public List<ModeloParaleloDTO> ConsultarModelosParalelosDAO()
        {
            try
            {
                var data = _context.ModeloParalelos.Select(
                    p => new ModeloParaleloDTO
                    {
                        id = p.paraleloId,
                        nombre = p.nombre,
                        cantidadAprobaciones = p.cantidadAprobaciones,
                        categoria = CategoriaMapper.EntityToDto(p.categoria)
                    }
                );

                return data.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }
        public ModeloParaleloDTO ActualizarModeloParaleloDAO(ModeloParalelo modeloParalelo)
        {
            try
            {
                _context.ModeloParalelos.Update(modeloParalelo);
                _context.DbContext.SaveChanges();

                return ModeloParaleloMapper.EntityToDto(modeloParalelo);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public ModeloParaleloDTO EliminarModeloParaleloDAO(string id)
        {
            try
            {
                var modeloParalelo = (ModeloParalelo)_context.ModeloParalelos.Where(
                    p => p.paraleloId == Guid.Parse(id)).First();
                _context.ModeloParalelos.Remove(modeloParalelo);
                _context.DbContext.SaveChanges();

                return ModeloParaleloMapper.EntityToDto(modeloParalelo);

            }
            catch (Exception ex)
            {
                Console.WriteLine("[Mensaje]: " + ex.Message + " [Seguimiento]: " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public ModeloParaleloDTO ConsultaModeloParaleloDAO(string id)
        {
            try
            {
                var modeloParalelo = _context.ModeloParalelos.Where(
                p => p.paraleloId == Guid.Parse(id)).First();
                return ModeloParaleloMapper.EntityToDto(modeloParalelo); ;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }
}