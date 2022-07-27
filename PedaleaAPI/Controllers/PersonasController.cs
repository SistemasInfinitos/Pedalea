using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity.Persona;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PedaleaAPI.Repository;
using PedaleaAPI.Repository.Persona;
using System.Data;
using System.Text.Json;

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
                personas = await _repository.GetPersonas();

                return personas;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return personas;
            }
        }

        [Route("[action]", Name = "GetPersonasById")]
        [HttpGet]
        public async Task<Personas> GetPersonasById(int PersonaID)
        {
            Personas personas = new Personas();
            try
            {
                personas = await _repository.GetPersonasById(PersonaID);

                return personas;
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

        [Route("[action]", Name = "UpdatePersona")]
        [HttpPut]
        public async Task<int> UpdatePersona(Personas entidad)
        {
            try
            {
                var data = await _repository.ActualizarPersona(entidad);

                return data;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return e.GetHashCode();
            }
        }

        [Route("[action]", Name = "DeletePersona")]
        [HttpPut]
        public async Task<int> DeletePersona(int PersonaID)
        {
            try
            {
                var data = await _repository.BorrarPersona(PersonaID);

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
