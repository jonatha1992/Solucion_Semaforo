using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLLCamara : IGestor<BECamara>
    {
        MPPCamara mPPCamara;

        public BLLCamara()
        {
            mPPCamara = new MPPCamara();
        }

        public string ObtenerUlitmoNroOrden(BEAeropuerto aer)
        {
            bool existe = true;
            var listaCamarasAeropuerto = ListarTodo().FindAll(x => x.Aeropuerto.Nombre == aer.Nombre);
            var nro = 1;
            while (existe)
            {
                if (listaCamarasAeropuerto.Exists(x => x.Orden == nro))
                {
                    existe = true;
                    nro++;
                }
                else
                {
                    break;
                }
            }

            return nro.ToString();
        }


        public List<BECamara> ListarTodo()
        {
            return mPPCamara.ListarTodo();
        }
        public List<BECamara> Buscar(string aer,string lugar, string denominacion )
        {
            var listadoCamaras = mPPCamara.ListarTodo().FindAll(x => x.Aeropuerto.Nombre == aer);
            listadoCamaras =  listadoCamaras.FindAll(x => x.Lugar.Contains(lugar) && x.Denominacion.Contains(denominacion));

            return listadoCamaras;
        }
        public BECamara ListarObjeto()
        {
            throw new NotImplementedException();
        }

        public bool Guardar(BECamara Entidad)
        {
            if (Entidad.Codigo == 0) //Nuevo
            {
                var Existe = ListarTodo().Exists(x => x.Orden == Entidad.Orden && x.Aeropuerto == Entidad.Aeropuerto || x.Denominacion == Entidad.Denominacion && x.Aeropuerto == Entidad.Aeropuerto);
                if (Existe)
                {
                    return false;
                }
                else
                { 
                    return mPPCamara.Guardar(Entidad);
                }
            }
            else //Modificar
            {
                return mPPCamara.Guardar(Entidad);

            }
        }

        
        public bool GuardarExcel(List<BECamara> lista)
        {
            return mPPCamara.GuardarExcel(lista);
        }
        public bool Eliminar(BECamara Entidad)
        {
            return mPPCamara.Eliminar(Entidad);
        }
    }
}
