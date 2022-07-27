using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanonicalModel.Model.Entity.Pedalea
{
    public class Documentos
    {
        public int? DocumentoID { get; set; }
        public int? PersonaIDCliente { get; set; }
        public int? PersonaIDVendedor { get; set; }
        public int? TipoDocumentoID { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
