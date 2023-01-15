using BE;
using Abstraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MPP
{
    public class MPPCamara : IGestor<BECamara>
    {
        XDocument Doc;

        public MPPCamara()
        {
            Doc = XDocument.Load("BaseDatos.xml");
        }
        public List<BECamara> ListarTodo()
        {
            try
            {
                Doc = XDocument.Load("BaseDatos.xml");
                var leer = from Camara in Doc.Descendants("Camaras").Descendants("Camara")
                           select new BECamara
                           {
                               Codigo = Convert.ToInt32(Convert.ToString(Camara.Attribute("Codigo").Value).Trim()),
                               Orden = Convert.ToInt32(Camara.Element("Orden").Value),
                               Denominacion = Convert.ToString(Camara.Element("Denominacion").Value).Trim(),
                               Descripcion = Convert.ToString(Camara.Element("Descripcion").Value).Trim(),
                               Lugar = Convert.ToString(Camara.Element("Lugar").Value).Trim(),
                               Fecha = (Convert.ToString(Camara.Element("Fecha_Reparacion").Value)) == "" ? (DateTime?)null :  Convert.ToDateTime(Convert.ToString(Camara.Element("Fecha_Reparacion").Value).Trim()),
                               Sistema = new BESistema(Convert.ToString(Camara.Element("Sistema").Value).Trim()),
                               Aeropuerto = new BEAeropuerto(Convert.ToString(Camara.Element("Aeropuerto").Value).Trim()),
                               Estado = new BEEstado(Convert.ToString(Camara.Element("Estado").Value).Trim()),
                           };

                var ListCamaras = leer.ToList<BECamara>();
                return ListCamaras;
            }
            catch (XmlException ex)
            {
                throw ex;
            }

        }

        public bool Guardar(BECamara bECamara)
        {
            try
            {
                if (bECamara.Codigo == 0) //Nuevo
                {
                    var listaCamarasAeropuerto = ListarTodo();
                    var nro = 1;
                    while (true)
                    {
                        if (listaCamarasAeropuerto.Exists(x => x.Codigo == nro))
                        {
                            nro++;
                        }
                        else
                        {
                            break;

                        }
                    }

                    bECamara.Codigo = nro;

                    Doc.Element("Base").Element("Camaras").Add(new XElement("Camara",
                                                new XAttribute("Codigo", bECamara.Codigo.ToString()),
                                                new XElement("Orden", bECamara.Orden.ToString()),
                                                new XElement("Denominacion", bECamara.Denominacion.ToString()),
                                                new XElement("Fecha_Reparacion", bECamara.Fecha.ToString()),
                                                new XElement("Descripcion", bECamara.Descripcion.ToString()),
                                                new XElement("Lugar", bECamara.Lugar.ToString()),
                                                new XElement("Sistema", bECamara.Sistema.ToString()),
                                                new XElement("Aeropuerto", bECamara.Aeropuerto.ToString()),
                                                new XElement("Estado", bECamara.Estado.ToString())
                                                ));

                }
                else
                {
                    var consulta = from Camara in Doc.Descendants("Camaras").Descendants("Camara")
                                   where Camara.Attribute("Codigo").Value == bECamara.Codigo.ToString()
                                   select Camara;

                    foreach (XElement EModifcar in consulta)
                    {
                        EModifcar.Element("Orden").Value = bECamara.Orden.ToString();
                        EModifcar.Element("Denominacion").Value = bECamara.Denominacion.ToString();
                        EModifcar.Element("Fecha_Reparacion").Value = bECamara.Fecha.ToString();
                        EModifcar.Element("Descripcion").Value = bECamara.Descripcion.ToString();
                        EModifcar.Element("Lugar").Value = bECamara.Lugar.ToString();
                        EModifcar.Element("Sistema").Value = bECamara.Sistema.ToString();
                        EModifcar.Element("Aeropuerto").Value = bECamara.Aeropuerto.ToString();
                        EModifcar.Element("Estado").Value = bECamara.Estado.ToString();
                    }
                }
                    Doc.Save("BaseDatos.xml");
                    return true;

            }
            catch (XmlException ex)
            {
                throw ex;
            }
        }

        public bool GuardarExcel(List<BECamara> lista)
        {
            try
            {
                foreach (var bECamara in lista)
                {
                    var consulta = from Camara in Doc.Descendants("Camaras").Descendants("Camara")
                                   where Camara.Attribute("Codigo").Value == bECamara.Codigo.ToString()
                                   select Camara;

                    foreach (XElement EModifcar in consulta)
                    {
                        EModifcar.Element("Orden").Value = bECamara.Orden.ToString();
                        EModifcar.Element("Denominacion").Value = bECamara.Denominacion.ToString();
                        EModifcar.Element("Fecha_Reparacion").Value = bECamara.Fecha.ToString();
                        EModifcar.Element("Descripcion").Value = bECamara.Descripcion.ToString();
                        EModifcar.Element("Lugar").Value = bECamara.Lugar.ToString();
                        EModifcar.Element("Sistema").Value = bECamara.Sistema.ToString();
                        EModifcar.Element("Aeropuerto").Value = bECamara.Aeropuerto.ToString();
                        EModifcar.Element("Estado").Value = bECamara.Estado.ToString();
                    }

                }
                Doc.Save("BaseDatos.xml");
                return true;

            }
            catch (XmlException ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(BECamara Becamara)
        {
            try
            {
                var consulta = from Jugador in Doc.Descendants("Camaras").Descendants("Camara")
                               where Jugador.Attribute("Codigo").Value == Becamara.Codigo.ToString()
                               select Jugador;

                consulta.Remove();
                Doc.Save("BaseDatos.xml");
                return true;
            }
            catch (XmlException ex)
            {
                throw ex;
            }
        }



        public BECamara ListarObjeto()
        {
            throw new NotImplementedException();
        }

    }
}
