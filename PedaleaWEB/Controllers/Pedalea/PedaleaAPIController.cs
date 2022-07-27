using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity.Pedalea;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PedaleaWEB.Controllers.Pedalea
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedaleaAPIController : ControllerBase
    {
        [Route("[action]", Name = "GetDocumentos")]
        [HttpGet]
        public async Task<IActionResult> GetDocumentos()
        {
            var options = new JsonSerializerOptions { IncludeFields = true, PropertyNameCaseInsensitive = true };
            await HttpContext.Session.LoadAsync();
            HttpContext.Session.SetString("token", "el token desde el login");

            string? accessToken = HttpContext.Session.GetString("token");
            AuthResult responseClient = new AuthResult();
            var model = new List<Documentos>();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                if (ModelState.IsValid)
                {
                    string uri = "https://localhost:7107/api/Pedalea/GetDocumentos"; // esto se debe configurar en el archivo json

                    try
                    {
                        var response = await httpClient.GetAsync(uri);

                        if (response.IsSuccessStatusCode)
                        {
                            model = await JsonSerializer.DeserializeAsync<List<Documentos>>(await response.Content.ReadAsStreamAsync(), options);

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
    }
}
