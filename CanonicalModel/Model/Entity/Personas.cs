using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanonicalModel.Model.Entity
{
    public class Personas
    {
        public int? PersonaID { get; set; }
        public int? Identificacion { get; set; }
        public string? PrimerNombre { get; set; } 
        public string? SegundoNombre { get; set; } 
        public string? PrimerApellido { get; set; } 
        public string? SegundoApellido { get; set; } 
        public bool? EsCliente { get; set; } 
        public bool? EsProveedor { get; set; } 
    }
}
