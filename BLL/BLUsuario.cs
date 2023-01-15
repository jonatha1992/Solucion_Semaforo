using Abstraccion;
using BE;
using MPP;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLLUsuario : IGestor<BEUsuario>
    {
        MPPUsuario mPPusuario;

        public BLLUsuario()
        {
            mPPusuario = new MPPUsuario();
        }

        public bool Verificar(BEUsuario usuario)
        {
            bool Existe = true;
            var listausuarios = ListarTodo();

            if (listausuarios.Exists(x => x.Nombre == usuario.Nombre && x.Password == usuario.Password))
            {
                Existe = true;
            }
            else
            {
                Existe = false;
            }

            return Existe;
        }


        public List<BEUsuario> ListarTodo()
        {
            return mPPusuario.ListarTodo();
        }

        public BEUsuario ListarObjeto()
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(BEUsuario Entidad)
        {
            return mPPusuario.Eliminar(Entidad);
        }

        public bool Guardar(BEUsuario Entidad)
        {
            throw new NotImplementedException();
        }
    }
}
