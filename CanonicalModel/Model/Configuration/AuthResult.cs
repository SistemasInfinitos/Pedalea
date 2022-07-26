using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanonicalModel.Model.Configuration
{
    public class AuthResult
    {
        public AuthResult()
        {
            Errors = new List<string>();
            Success = false;
        }

        public bool? Success { get; set; }
        public List<string>? Errors { get; set; }
        public string? Mensaje { get; set; }

    }
}
