using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    [Serializable]
    public class RedireccionamientoException : PedidosEmpanadasException
    {
        public string PathRedirect { get; set; }

        public RedireccionamientoException()
        {
        }

        public RedireccionamientoException(string path)
        {
            PathRedirect = path;
        }

        
    }
}
