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

        /// <summary>
        /// retorna el docuemnto seleccionado
        /// </summary>
        /// <param name="DocumentoID"></param>
        /// <returns></returns>
        Task<Documentos> GetDocumentosById(int DocumentoID);
        
        /// <summary>
        /// trae todos los productos o los valores filtrados
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<Productos>> GetProductosByName(string name,int? ProductoID);

        Task<int> BorrarDocumentosById(int DocumentoID);
    }
}
