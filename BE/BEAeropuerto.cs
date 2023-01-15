using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEAeropuerto
    {
        public BEAeropuerto( string aer)
        {
            Nombre = aer;
        }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
