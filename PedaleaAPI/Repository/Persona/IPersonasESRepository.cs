using CanonicalModel.Model.Entity;

namespace PedaleaAPI.Repository.Persona
{
    public interface IPersonasESRepository
    {
        Task<int> CrearPersona(Personas entidad);

        Task<List<Personas>> GetPersonas();
    }
}
