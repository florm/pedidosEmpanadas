using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Exceptions;

namespace BackendTp
{
    public class PedidosEmpanadasExceptionResponse
    {
        public string Mensaje { get; set; }

        public PedidosEmpanadasExceptionResponse(PedidosEmpanadasException exception)
        {
            Mensaje = exception.Message;
        }
    }
}