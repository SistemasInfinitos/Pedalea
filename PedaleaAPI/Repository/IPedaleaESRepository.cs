using CanonicalModel.Model.Entity;

namespace PedaleaAPI.Repository
{
    public interface IPedaleaESRepository
    {

        Task<int> CrearPersona(Personas entidad);

        Task<List<Personas>> GetPersonas();
    }
}
