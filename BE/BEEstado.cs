using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEEstado
    {
        public BEEstado(string estado)
        {
            Estado = estado;
        }

        public string Estado{ get; set; }
        public override string ToString()
        {
            return Estado;
        }
    }
}
