using ServicesDeskUCABWS.Persistence.DAO.Interface;
using ServicesDeskUCABWS.Persistence.Database;
using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Implementations
{
    public class CargoDao : ICargoDAO
    {
        public readonly MigrationDbContext _context;

        
        public CargoDao(MigrationDbContext dbcontext)
        {
            _context = dbcontext;
        }
          public   bool CargoExist(int id){
            return _context.Cargos.Any(p=>p.id == id);
            }
         public ICollection<Cargo> GetCargos(){
            return _context.Cargos.OrderBy(p=>p.id).ToList();
          }
         public Cargo  GetCargo(int id){
            return _context.Cargos.Where(p=>p.id==id).FirstOrDefault();
         }
        public  ICollection<Usuario> GetUsuarioByCargo(int cargoid){
            return _context.Usuario.Where(p=>p.cargo.id==cargoid).ToList();

         }
        
    }
}