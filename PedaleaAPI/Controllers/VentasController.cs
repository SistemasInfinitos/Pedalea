using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PedaleaAPI.Repository;
using System.Data;

namespace PedaleaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly JwtConfiguration _jwtConfig;
        private readonly IPedaleaESRepository _repository;

        public VentasController(IOptionsMonitor<JwtConfiguration> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _repository = new PedaleaESRepository(optionsMonitor);
        }

        [Route("[action]", Name = "GetPersonas")]
        [HttpGet]
        public async Task<List<Personas>> GetPersonas()
        {
            List<Personas> personas= new List<Personas>();
            try
            {
                var data = await _repository.GetPersonas();

                return  data;
            }
            catch (EvaluateException e)
            {
                var debugger = e.Message;
                return personas;
            }
        }
    }
}
