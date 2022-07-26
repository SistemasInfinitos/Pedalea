using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity.Persona;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace PedaleaWEB.Controllers.StorePedalea
{
    public class PedaleaController : Controller
    {
        [HttpGet]
        public IActionResult Gestion()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetPersonas()
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
                                return Json(new { model, responseClient });
                            }
                            else
                            {
                                responseClient.Errors.Add("Acceso denegado!");
                                return Json(new { model, responseClient });
                            }
                        }
                        else
                        {
                            responseClient.Errors = new List<string>() { "Acceso denegado!" };
                            responseClient.Errors.Add(response.EnsureSuccessStatusCode().StatusCode.ToString());

                            return Json(new { model, responseClient });
                        }
                    }
                    catch (Exception x)
                    {
                        responseClient.Errors.Add(x.Message);
                        responseClient.Errors.Add(x.InnerException?.Message ?? "Exception!");
                        return Json(new { model = 0, responseClient });
                    }
                }
            }

            responseClient.Errors.Add("Su sesión a Caducado, Acceso denegado!");
            return Json(new { model, responseClient });
        }

        [HttpPost]
        public async Task<JsonResult> CrearPersonas([FromBody] Personas entidad)
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
                    string uri = "https://localhost:7107/api/Personas/InserPersonas";// esto se debe configurar en el archivo json

                    try
                    {
                        var json = JsonSerializer.Serialize(entidad, options);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(uri, data);

                        if (response.IsSuccessStatusCode)
                        {
                            model = await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync(), options);

                            if (model > 0)
                            {
                                responseClient.Success = true;
                                responseClient.Mensaje = "success";
                                return Json(new { model, responseClient });
                            }
                            else
                            {
                                responseClient.Errors.Add("Acceso denegado!");
                                return Json(new { model, responseClient });
                            }
                        }
                        else
                        {
                            responseClient.Errors = new List<string>() { "Acceso denegado!" };
                            responseClient.Errors.Add(response.EnsureSuccessStatusCode().StatusCode.ToString());

                            return Json(new { model, responseClient });
                        }
                    }
                    catch (Exception x)
                    {
                        responseClient.Errors.Add(x.Message);
                        responseClient.Errors.Add(x.InnerException?.Message ?? "Exception!");
                        return Json(new { model = 0, responseClient });
                    }
                }
            }

            responseClient.Errors.Add("Su sesión a Caducado, Acceso denegado!");
            return Json(new { model, responseClient });
        }
    }
}
