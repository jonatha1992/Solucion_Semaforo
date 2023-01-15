using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BENovedad
    {
        public BENovedad() { }
        public BENovedad( int cod)
        {
            Codigo = cod;
        }
        public int Codigo { get; set; }
        //public BEEstado Estado { get; set; }
        public DateTime? Fecha_novedad { get; set; }
        public DateTime? Fecha_corte { get; set; }
        public string Novedad { get; set; }

        public override string ToString()
        {
            return Novedad;
        }
    }
}
