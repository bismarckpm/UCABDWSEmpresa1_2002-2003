using ServicesDeskUCABWS.Persistence.Entity;

namespace ServicesDeskUCABWS.Persistence.DAO.Interface
{
    public interface ICargoDAO
    {
         ICollection<Cargo> GetCargos();
         Cargo  GetCargo(int id);
         ICollection<Usuario> GetUsuarioByCargo(int cargoid); 
            bool CargoExist(int id);

    }
}