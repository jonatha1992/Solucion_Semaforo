using BE;
using Abstraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MPP
{
    public class MPPUsuario : IGestor<BEUsuario>
    {
        XDocument Doc;

        public MPPUsuario()
        {
            Doc = XDocument.Load("BaseDatos.xml");
        }

        public bool Eliminar(BEUsuario Entidad)
        {
            throw new NotImplementedException();
        }

        public bool Guardar(BEUsuario Entidad)
        {
            throw new NotImplementedException();
        }

        public BEUsuario ListarObjeto()
        {
            throw new NotImplementedException();
        }

        public List<BEUsuario> ListarTodo()
        {
            try
            {
                Doc = XDocument.Load("BaseDatos.xml");
                var leer = from Camara in Doc.Descendants("Usuarios").Descendants("Usuario")
                           select new BEUsuario
                           {
                               Nombre = Convert.ToString(Camara.Element("Nombre").Value).Trim(),
                               Password = Convert.ToString(Camara.Element("Password").Value).Trim(),
                           };

                var ListUsuario = leer.ToList<BEUsuario>();
                return ListUsuario;
            }
            catch (XmlException ex)
            {
                throw ex;
            }

        }
    }
}
