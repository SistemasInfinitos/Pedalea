using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PedaleaAPI.Repository;
using PedaleaAPI.Repository.Persona;
using System.Data;

namespace PedaleaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly IPersonasESRepository _repository;

        public PersonasController(IOptionsMonitor<JwtConfiguration> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _repository = new PersonasESRepository(optionsMonitor);
        }

        #region Persona Metodos
        [Route("[action]", Name = "GetPersonas")]
        [HttpGet]
        public async Task<List<Personas>> GetPersonas()
        {
            List<Personas> personas = new List<Personas>();
            try
            {
                var data = await _repository.GetPersonas();

                return data;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return personas;
            }
        }


        [Route("[action]", Name = "IsertPersona")]
        [HttpPost]
        public async Task<int> IsertPersona(Personas entidad)
        {
            try
            {
                var data = await _repository.CrearPersona(entidad);

                return data;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return e.GetHashCode();
            }
        }
        #endregion
    }
}
