using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraccion
{
    public interface IGestor<T>
    {
        List<T> ListarTodo();
        T ListarObjeto();
        bool Guardar (T Entidad) ;
        bool Eliminar(T Entidad);
    }
}
