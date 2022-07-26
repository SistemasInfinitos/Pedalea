using CanonicalModel.Model.Entity.Persona;

namespace PedaleaAPI.Repository
{
    public interface IPedaleaESRepository
    {

        Task<int> Crear(Personas entidad);

        Task<List<Personas>> Get();
    }
}
