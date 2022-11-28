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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al Consultar: " + ex.Message, ex);
            }
        }

        public DepartamentoDTO ModificarDepartamentoDAO(Departamento departamento)
        {
            try
            {
                _context.Departamentos.Update(departamento);
                _context.DbContext.SaveChanges();

                return DepartamentoMapper.EntityToDto(departamento);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public DepartamentoDTO EliminarDepartamentoDAO(int id)
        {
            try
            {
                var departamento = _context.Departamentos.Where(
                    d => d.id == id).First();
                _context.Departamentos.Remove(departamento);
                _context.DbContext.SaveChanges();

                return DepartamentoMapper.EntityToDto(departamento);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " || " + ex.StackTrace);
                throw new Exception("Fallo al Eliminar Departamento: " + id, ex);
            }
        }

        public DepartamentoDTO ConsultaUnDepartamentoDAO(int id)
        {
            try
            {
                var departamento = _context.Departamentos.Where(
                d => d.id == id).First();
                return DepartamentoMapper.EntityToDto(departamento); ;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al Consultar por id: " + id, ex);
            }
        }

    }
}