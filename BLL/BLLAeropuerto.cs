using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLLAeropuerto : IGestor<BEAeropuerto>
    {
        MPPAeropuerto mPPAeropuerto;

        public BLLAeropuerto()
        {
            mPPAeropuerto = new MPPAeropuerto();
        }

        public List<BEAeropuerto> ListarTodo()
        {
            return mPPAeropuerto.ListarTodo();
        }

        public BEAeropuerto ListarObjeto()
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(BEAeropuerto Entidad)
        {
            return mPPAeropuerto.Eliminar(Entidad);
        }

        public bool Guardar(BEAeropuerto Entidad)
        {
            throw new NotImplementedException();
        }
    }
}
