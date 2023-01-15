using BE;
using Abstraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MPP
{
    public class MPPNovedad : IGestor<BENovedad>
    {
        XDocument Doc;

        public MPPNovedad()
        {
        }

        public bool Eliminar(BENovedad Entidad)
        {
            throw new NotImplementedException();
        }

        public bool Guardar(BENovedad Entidad)
        {
            throw new NotImplementedException();
        }

        public BENovedad ListarObjeto()
        {
            throw new NotImplementedException();
        }

        public List<BENovedad> ListarTodo(BECamara camara)
        {
            try
            {
                Doc = XDocument.Load("BaseDatos.xml");

                var leer = from Novedad in Doc.Descendants("Novedades").Descendants("Novedad")
                           where Novedad.Element("Id_camara").Value == camara.Codigo
                           select new BENovedad
                           {
                               Codigo = Convert.ToInt32(Convert.ToString(Novedad.Attribute("Codigo").Value).Trim()),
                               Fecha_novedad = Convert.ToDateTime(Convert.ToString(Novedad.Element("Fecha_novedad").Value).Trim()),
                               Fecha_corte = Convert.ToDateTime(Convert.ToString(Novedad.Element("Fecha_corte").Value).Trim()),
                               Novedad = Convert.ToString(Novedad.Element("Observacion").Value).Trim()
                           };

                var ListNovedad = leer.ToList<BENovedad>();
                
                Doc.Save("BaseDatos.xml");
                return ListNovedad;
            }
            catch (XmlException ex)
            {
                throw ex;
            }

        }

        public List<BENovedad> ListarTodo()
        {
            throw new NotImplementedException();
        }
    }
}
