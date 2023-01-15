using BE;
using Abstraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MPP
{
    public class MPPAeropuerto : IGestor<BEAeropuerto>
    {
        XDocument Doc;

        public MPPAeropuerto()
        {
        }

        public bool Eliminar(BEAeropuerto Entidad)
        {
            throw new NotImplementedException();
        }

        public bool Guardar(BEAeropuerto Entidad)
        {
            throw new NotImplementedException();
        }

        public BEAeropuerto ListarObjeto()
        {
            throw new NotImplementedException();
        }

        public List<BEAeropuerto> ListarTodo()
        {
            try
            {
                Doc = XDocument.Load("BaseDatos.xml");

                var leer = from Camara in Doc.Descendants("Aeropuertos").Descendants("Aeropuerto")
                           select new BEAeropuerto
                           {
                               Codigo = Convert.ToString(Camara.Element("Codigo").Value).Trim(),
                               Nombre = Convert.ToString(Camara.Element("Nombre").Value).Trim()
                           };

                var ListAeropuerto = leer.ToList<BEAeropuerto>();
                
                Doc.Save("BaseDatos.xml");
                return ListAeropuerto;
            }
            catch (XmlException ex)
            {
                throw ex;
            }

        }
    }
}
