using CanonicalModel.Model.Entity.Pedalea;
using CanonicalModel.Model.Entity.Persona;

namespace PedaleaAPI.Repository.Pedalea
{
    public interface IPedaleaESRepository
    {
        /// <summary>
        /// Crea un documento con su detalle
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task<int> CrearDocumento(Documentos entidad);

        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <returns></returns>
        Task<List<Documentos>> GetDocumentos();
    }
}
