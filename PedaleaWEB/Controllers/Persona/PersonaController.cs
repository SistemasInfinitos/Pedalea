using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity.Persona;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace PedaleaWEB.Controllers.Persona
{
    public class PersonaController : Controller
    {
        [HttpGet]
        public IActionResult Gestion()
        {
            return View();
        }

    }
}
