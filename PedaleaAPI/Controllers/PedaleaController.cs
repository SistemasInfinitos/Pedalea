using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity;
using CanonicalModel.Model.Entity.Pedalea;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PedaleaAPI.Repository;
using PedaleaAPI.Repository.Pedalea;
using System.Data;

namespace PedaleaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedaleaController : ControllerBase
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly IPedaleaESRepository _repository;

        public PedaleaController(IOptionsMonitor<JwtConfiguration> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _repository = new PedaleaESRepository(optionsMonitor);
        }

        #region Documento
        [Route("[action]", Name = "GetDocumentos")]
        [HttpGet]
        public async Task<List<Documentos>> GetDocumentos()
        {
            List<Documentos> personas = new List<Documentos>();
            try
            {
                personas = await _repository.GetDocumentos();

                return personas;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return personas;
            }
        }


        [Route("[action]", Name = "GetDocumentoById")]
        [HttpGet]
        public async Task<Documentos> GetDocumentoById(int DocumentoID)
        {
            Documentos personas = new Documentos();
            try
            {
                personas = await _repository.GetDocumentosById(DocumentoID);

                return personas;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return personas;
            }
        }

        [Route("[action]", Name = "DeleteDocumento")]
        [HttpDelete]
        public async Task<int> DeleteDocumento(int DocumentoID)
        {
            try
            {
                var data = await _repository.BorrarDocumentosById(DocumentoID);

                return data;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return e.GetHashCode();
            }
        }

        [Route("[action]", Name = "GetProductoByName")]
        [HttpGet]
        public async Task<List<Productos>> GetProductoByName(string name, int? ProductoID)
        {
            List<Productos> personas = new List<Productos>();
            try
            {
                personas = await _repository.GetProductosByName(name,ProductoID);

                return personas;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return personas;
            }
        }
        #endregion
    }
}
