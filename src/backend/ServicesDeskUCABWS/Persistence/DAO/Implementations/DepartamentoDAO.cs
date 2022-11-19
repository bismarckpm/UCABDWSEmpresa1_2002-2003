using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class DepartamentoDAO : IDepartamentoDAO
    {
        private readonly IMigrationDbContext _context;

        public DepartamentoDAO(IMigrationDbContext context)
        {
            this._context = context;
        }

        public DepartamentoDTO AgregarDepartamentoDAO(Departamento departamento)
        {
            try
            {
                 _context.Departamentos.Add(departamento);
                _context.DbContext.SaveChanges();

                var data = _context.Departamentos.Where(a => a.id == departamento.id)
                .Select(
                        a => new DepartamentoDTO
                        {
                            Id = a.id,
                            Nombre = a.nombre
                        }
                    );   

                    return data.First();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Transaccion Fallida", ex);
            }
        }

        public List<DepartamentoDTO> ConsultarDepartamentosDAO()
        {
            try
            {
                    var data = _context.Departamentos.Select(
                        d => new DepartamentoDTO
                        {
                            Id = d.id,
                            Nombre = d.nombre
                        }
                    );

                    return data.ToList();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex.InnerException!;
            }
        }

        public DepartamentoDTO ModificarDepartamentoDAO(Departamento departamento)
        {
            try
            {
                
                _context.Departamentos.Update(departamento);
                _context.DbContext.SaveChanges();

                return DepartamentoMapper.EntityToDto(departamento);

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public DepartamentoDTO EliminarDepartamentoDAO(int id)
        {
            try
            {
                    var departamento = (Departamento) _context.Departamentos.Where(
                        d=> d.id == id).First();
                        _context.Departamentos.Remove(departamento);
                        _context.DbContext.SaveChanges();

                        return DepartamentoMapper.EntityToDto(departamento);
                        
            }catch(Exception ex)
            {
                Console.WriteLine("[Mensaje]: " + ex.Message + " [Seguimiento]: " + ex.StackTrace);
                DepartamentoDTO errorDTO = new DepartamentoDTO();
                errorDTO.Nombre = "Error al Eliminar Departamento";
                return errorDTO;
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
    }
}