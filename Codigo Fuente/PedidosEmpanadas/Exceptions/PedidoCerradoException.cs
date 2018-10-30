using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Exceptions
{
    public class PedidoCerradoException : Exception
    {
        public PedidoCerradoException()
        {
           
        }

        public override string Message => "Pedido cerrado";
    }
}
