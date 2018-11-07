using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Exceptions
{
    public class PedidoCerradoException : PedidosEmpanadasException
    {
        public PedidoCerradoException()
        {
           
        }

        public override string Message => "El Pedido se encuentra cerrado";
    }
}
