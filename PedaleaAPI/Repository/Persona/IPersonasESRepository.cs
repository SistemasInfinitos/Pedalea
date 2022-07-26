using CanonicalModel.Model.Entity.Persona;

namespace PedaleaAPI.Repository.Persona
{
    public interface IPersonasESRepository
    {
        /// <summary>
        /// crea una nueva persona
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task<int> CrearPersona(Personas entidad);

        /// <summary>
        /// obtien todas la personas
        /// </summary>
        /// <returns></returns>
        Task<List<Personas>> GetPersonas();

        /// <summary>
        /// Obtiene la persona seleccionada
        /// </summary>
        /// <param name="PersonaID"></param>
        /// <returns></returns>
        Task<Personas> GetPersonasById(int PersonaID);
    }
}
