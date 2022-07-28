using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanonicalModel.Model.Entity.Pedalea
{
    public class Productos
    {
        public Productos()
        {
            Valor = 0;
            ValorUnitario = 0;
            Cantidad = 0;
            PorcentajeDescuento = 0;
            Neto = 0;
        }

        public int? ProductoID { get; set; }
        public string? Producto { get; set; }
        public decimal? Valor { get; set; }
        public string? Talla { get; set; }
        public string? Color { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public decimal? ValorUnitario { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? PorcentajeDescuento { get; set; }
        public decimal? Neto { get; set; }
    }
}
