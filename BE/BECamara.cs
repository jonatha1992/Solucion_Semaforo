using System;
using System.Collections.Generic;

namespace BE
{
    public class BECamara : ICloneable, IComparable<BECamara>
    {
        public BECamara() { }
        public BECamara(int cod) { Codigo = cod; }
        public int Codigo { get; set; }
        public int Orden { get; set; }
        public string Denominacion { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public BESistema Sistema { get; set; }
        public BEAeropuerto Aeropuerto { get; set; }
        public BEEstado Estado { get; set; }
        public DateTime? Fecha { get; set; }

        public object Clone() // Clonacion superficial
        {
            return (BECamara)this.MemberwiseClone();
        }

        //public object Clone() // Clonacion Profunda
        //{   
        //    var clone = (BECamara)this.MemberwiseClone();
        //    clone.Sistema = this.Sistema;
        //    return clone;
        //}
        public int CompareTo(BECamara other)
        {
            if (other.Codigo > this.Codigo) return 1;
            else if (other.Codigo < this.Codigo) return -1;
            else return 0;
        }

        public override string ToString()
        {
            return  Denominacion;
        }

    }
}