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

        #region Tienda
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
        #endregion
    }
}
