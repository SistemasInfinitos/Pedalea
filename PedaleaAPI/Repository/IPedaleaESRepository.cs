using CanonicalModel.Model.Entity;

namespace PedaleaAPI.Repository
{
    public interface IPedaleaESRepository
    {

        Task<Personas> CrearPersona(Personas entidad);

        Task<List<Personas>> GetPersonas();
    }
}
