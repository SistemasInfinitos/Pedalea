using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanonicalModel.Model.Entity.Persona
{
    public partial struct DataTableResponsePersona
    {
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public List<Personas> data;
    }
}
