using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity.Persona;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace PedaleaWEB.Controllers.StorePedalea
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedaleaAPIController : ControllerBase
    {
        [Route("[action]", Name = "GetPersonas")]
        [HttpGet]
        public async Task<IActionResult> GetPersonas()
        {
            var options = new JsonSerializerOptions { IncludeFields = true, PropertyNameCaseInsensitive = true };
            await HttpContext.Session.LoadAsync();
            HttpContext.Session.SetString("token", "el token desde el login");

            string? accessToken = HttpContext.Session.GetString("token");
            AuthResult responseClient = new AuthResult();
            var model = new List<Personas>();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                if (ModelState.IsValid)
                {
                    string uri = "https://localhost:7107/api/Personas/GetPersonas"; // esto se debe configurar en el archivo json

                    try
                    {
                        var response = await httpClient.GetAsync(uri);

                        if (response.IsSuccessStatusCode)
                        {
                            model = await JsonSerializer.DeserializeAsync<List<Personas>>(await response.Content.ReadAsStreamAsync(), options);

                            if (model?.Count() > 0)
                            {
                                responseClient.Success = true;
                                responseClient.Mensaje = "success";
                                return Ok(new { model, responseClient });
                            }
                            else
                            {
                                responseClient.Errors.Add("Acceso denegado!");
                                return BadRequest(new { model, responseClient });
                            }
                        }
                        else
                        {
                            responseClient.Errors = new List<string>() { "Acceso denegado!" };
                            responseClient.Errors.Add(response.EnsureSuccessStatusCode().StatusCode.ToString());

                            return BadRequest(new { model, responseClient });
                        }
                    }
                    catch (Exception x)
                    {
                        responseClient.Errors.Add(x.Message);
                        responseClient.Errors.Add(x.InnerException?.Message ?? "Exception!");
                        return BadRequest(new { model, responseClient });
                    }
                }
            }

            responseClient.Errors.Add("Su sesión a Caducado, Acceso denegado!");
            return BadRequest(new { model, responseClient });
        }

        [HttpPost]
        [Route("[action]")]
        public string CrearPersonasA()
        {
            string value="hola";
            return value;

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CrearPersonasB([FromBody] Personas value)
        {

            return Ok(value);

        }


        [HttpPost]
        [Route("[action]")]   
        public async Task<IActionResult> CrearPersonas([FromBody] Personas value)
        {
            var options = new JsonSerializerOptions { IncludeFields = true, PropertyNameCaseInsensitive = true };
            await HttpContext.Session.LoadAsync();
            HttpContext.Session.SetString("token", "el token desde el login");

            string? accessToken = HttpContext.Session.GetString("token");
            AuthResult responseClient = new AuthResult();
            var model = new int();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                if (ModelState.IsValid)
                {
                    string uri = "https://localhost:7107/api/Personas/IsertPersona";// esto se debe configurar en el archivo json

                    try
                    {
                        var json = JsonSerializer.Serialize(value, options);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(uri, data);

                        if (response.IsSuccessStatusCode)
                        {
                            model = await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync(), options);

                            if (model > 0)
                            {
                                responseClient.Success = true;
                                responseClient.Mensaje = "Transacción exitosa!";
                                return Ok(new { model, responseClient });
                            }
                            else
                            {
                                responseClient.Errors.Add("Acceso denegado!");
                                return BadRequest(new { model, responseClient });
                            }
                        }
                        else
                        {
                            responseClient.Errors = new List<string>() { "Acceso denegado!" };
                            responseClient.Errors.Add(response.EnsureSuccessStatusCode().StatusCode.ToString());

                            return BadRequest(new { model, responseClient });
                        }
                    }
                    catch (Exception x)
                    {
                        responseClient.Errors.Add(x.Message);
                        responseClient.Errors.Add(x.InnerException?.Message ?? "Exception!");
                        return BadRequest(new { model, responseClient });
                    }
                }
            }

            responseClient.Errors.Add("Su sesión a Caducado, Acceso denegado!");
            return BadRequest(new { model, responseClient });
        }
    }
}
