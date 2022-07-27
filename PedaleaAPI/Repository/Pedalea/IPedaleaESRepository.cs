using CanonicalModel.Model.Entity.Pedalea;
using CanonicalModel.Model.Entity.Persona;

namespace PedaleaAPI.Repository.Pedalea
{
    public interface IPedaleaESRepository
    {

        Task<int> CrearDocumento(Documentos entidad);

        Task<List<Documentos>> GetDocumentos();
    }
}
