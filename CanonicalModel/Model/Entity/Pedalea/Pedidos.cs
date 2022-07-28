using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanonicalModel.Model.Entity.Pedalea
{
    public class Pedidos
    {
        public Pedidos() {
            LisPedidos = new();
        }
        public int? DocumentoID { get; set; }
        public decimal ValorTotal { get; set; }
        public int? TipoDocumentoID { get; set; }
        public int? PersonaIDCliente { get; set; }
        public int? PersonaIDVendedor { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion { get; set; }
        public bool? EsCliente { get; set; }
        public bool? EsProveedor { get; set; }

        public List<Productos?>? LisPedidos { get; set; }
    }
}

