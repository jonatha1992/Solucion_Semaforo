using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BESistema
    {   
        public BESistema(string sis) 
        {
            Sistema = sis;
        }
        public string Sistema { get; set; }
        public override string ToString()
        {
            return Sistema;
        }
    }
}
