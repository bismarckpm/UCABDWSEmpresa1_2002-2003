using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations;

public class ModeloJerarquicoDAO : IModeloJerarquicoDAO
{
    private readonly IMigrationDbContext _context;
    public ModeloJerarquicoDAO(IMigrationDbContext context)
    {
        this._context = context;
    }
    public ModeloJerarquicoDTO AgregarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico)
    {
        try
        {
            _context.ModeloJerarquicos.Add(modeloJerarquico);
            _context.DbContext.SaveChanges();

            var data = _context.ModeloJerarquicos.Where(a => a.jerarquicoId == modeloJerarquico.jerarquicoId)
            .Select(
                    a => new ModeloJerarquicoDTO
                    {
                        id = a.jerarquicoId,
                        nombre = a.nombre,
                        orden = a.orden,
                        tipoCargo = a.tipoCargo,
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

         public List<ModeloJerarquicoDTO> ConsultarModelosJerarquicosDAO()
        {
            try
            {
                var data = _context.ModeloJerarquicos.Select(
                    p => new ModeloJerarquicoDTO
                    {
                        id = p.jerarquicoId,
                        nombre = p.nombre,
                        orden = p.orden,
                        tipoCargo = p.tipoCargo,
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
        public ModeloJerarquicoDTO ActualizarModeloJerarquicoDAO(ModeloJerarquico modeloJerarquico)
        {
            try
            {
                _context.ModeloJerarquicos.Update(modeloJerarquico);
                _context.DbContext.SaveChanges();

                return ModeloJerarquicoMapper.EntityToDto(modeloJerarquico);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public ModeloJerarquicoDTO EliminarModeloJerarquicoDAO(string id)
        {
            try
            {
                var modeloJerarquico = (ModeloJerarquico)_context.ModeloJerarquicos.Where(
                    p => p.jerarquicoId == Guid.Parse(id)).First();
                _context.ModeloJerarquicos.Remove(modeloJerarquico);
                _context.DbContext.SaveChanges();

                return ModeloJerarquicoMapper.EntityToDto(modeloJerarquico);

            }
            catch (Exception ex)
            {
                Console.WriteLine("[Mensaje]: " + ex.Message + " [Seguimiento]: " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public ModeloJerarquicoDTO ConsultaModeloJerarquicoDAO(string id)
        {
            try
            {
                var modeloJerarquico = _context.ModeloJerarquicos.Where(
                p => p.jerarquicoId == Guid.Parse(id)).First();
                return ModeloJerarquicoMapper.EntityToDto(modeloJerarquico); ;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }
}