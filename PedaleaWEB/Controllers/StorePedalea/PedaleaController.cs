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

    }
}
