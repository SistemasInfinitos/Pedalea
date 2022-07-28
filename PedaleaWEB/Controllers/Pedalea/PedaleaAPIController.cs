using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity.Pedalea;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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

        [Route("[action]", Name = "GetDocumentosById")]
        [HttpGet]
        public async Task<IActionResult> GetDocumentosById(int DocumentoID)
        {
            var options = new JsonSerializerOptions { IncludeFields = true, PropertyNameCaseInsensitive = true };
            await HttpContext.Session.LoadAsync();
            HttpContext.Session.SetString("token", "el token desde el login");

            string? accessToken = HttpContext.Session.GetString("token");
            AuthResult responseClient = new AuthResult();
            var model = new Documentos();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                if (ModelState.IsValid)
                {
                    string uri = $"https://localhost:7107/api/Pedalea/GetDocumentoById=DocumentoID={DocumentoID}"; // esto se debe configurar en el archivo json

                    try
                    {
                        var response = await httpClient.GetAsync(uri);

                        if (response.IsSuccessStatusCode)
                        {
                            model = await JsonSerializer.DeserializeAsync<Documentos>(await response.Content.ReadAsStreamAsync(), options);

                            if (model!=null && model.DocumentoID > 0)
                            {
                                responseClient.Success = true;
                                responseClient.Mensaje = "success";
                                return Ok(new { model, responseClient });
                            }
                            else if (model != null && (model.DocumentoID == 0|| model.DocumentoID == null))
                            {
                                responseClient.Success = true;
                                responseClient.Mensaje = "Sin datos";
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
        
        [Route("[action]", Name = "GetProductoByName")]
        [HttpGet]
        public async Task<IActionResult> GetProductoByName(string name, int? ProductoID)
        {
            var options = new JsonSerializerOptions { IncludeFields = true, PropertyNameCaseInsensitive = true };
            await HttpContext.Session.LoadAsync();
            HttpContext.Session.SetString("token", "el token desde el login");

            string? accessToken = HttpContext.Session.GetString("token");
            AuthResult responseClient = new AuthResult();
            var model = new List<Productos>();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                if (ModelState.IsValid)
                {
                    string uri = $"https://localhost:7107/api/Pedalea/GetProductoByName?name={name}&ProductoID={ProductoID}"; // esto se debe configurar en el archivo json

                    try
                    {
                        var response = await httpClient.GetAsync(uri);

                        if (response.IsSuccessStatusCode)
                        {
                            model = await JsonSerializer.DeserializeAsync<List<Productos>>(await response.Content.ReadAsStreamAsync(), options);

                            if (model!=null && model.Count > 0)
                            {
                                responseClient.Success = true;
                                responseClient.Mensaje = "success";
                                return Ok(new { model, responseClient });
                            }
                            else if (model == null || model.Count == 0)
                            {
                                responseClient.Success = false;
                                responseClient.Mensaje = "Sin datos";
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

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> BorrarDocumento(int DocumentoID)
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
                    string uri = $"https://localhost:7107/api/Pedalea/DeleteDocumento?DocumentoID={DocumentoID}";// esto se debe configurar en el archivo json

                    try
                    {
                        var json = JsonSerializer.Serialize(DocumentoID, options);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.DeleteAsync(uri);

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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CrearDocumento([FromBody] Pedidos value)
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
                    string uri = "https://localhost:7107/api/Pedalea/DeleteDocumento";// esto se debe configurar en el archivo json

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
