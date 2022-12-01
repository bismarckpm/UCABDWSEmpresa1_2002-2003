using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Entity;
using ServicesDeskUCABWS.BussinessLogic.DTO;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using ServicesDeskUCABWS.Persistence.Database;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class GrupoDAO : IGrupoDAO
    {
        private readonly IMigrationDbContext _context;

        public GrupoDAO(IMigrationDbContext context)
        {
            this._context = context;
        }

        public GrupoDTO AgregarGrupoDAO(Grupo grupo)
        {
            try
            {
                _context.Grupo.Add(grupo);
                _context.DbContext.SaveChanges();

                var data = _context.Grupo.Where(a => a.id == grupo.id)
                .Select(
                        a => new GrupoDTO
                        {
                            id = a.id,
                            nombre = a.nombre,
                            departamentoid = a.departamentoid
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

        public List<GrupoDTO> ConsultarGrupoDAO()
        {
            try
            {
                var data = _context.Grupo.Select(
                    g => new GrupoDTO
                    {
                        id = g.id,
                        nombre = g.nombre,
                        departamentoid = g.departamentoid
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

        public GrupoDTO ActualizarGrupoDAO(Grupo grupo)
        {
            try
            {
                _context.Grupo.Update(grupo);
                _context.DbContext.SaveChanges();

                return GrupoMapper.EntityToDto(grupo);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                throw new Exception("Transaccion Fallo", ex)!;
            }
        }

        public GrupoDTO EliminarGrupoDAO(int id)
        {
            try
            {
                var grupo = _context.Grupo.Where(
                    g => g.id == id).First();
                _context.Grupo.Remove(grupo);
                _context.DbContext.SaveChanges();

                return GrupoMapper.EntityToDto(grupo);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " || " + ex.StackTrace);
                throw new Exception("Fallo al Eliminar Departamento: " + id, ex);
            }
        }

        public GrupoDTO ConsultaGrupoIdDAO(int id)
        {
            try
            {
                var grupo = _context.Grupo.Where(
                d => d.id == id).First();
                return GrupoMapper.EntityToDto(grupo); ;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al Consultar por id: " + id, ex);
            }
        }

    }

}
