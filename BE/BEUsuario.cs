using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEUsuario
    {

        public BEUsuario() { }
        public BEUsuario(string nombre , string pass ) {
            Nombre = nombre; Password = pass; 
        }
        public string Nombre{ get; set; }
        public string Password{ get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
