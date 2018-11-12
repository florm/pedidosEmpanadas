using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Exceptions
{
    public class PermisosException : PedidosEmpanadasException
    {
        public PermisosException()
        {
           
        }

        public override string Message => "No posee permisos para ver esta página";
    }
}
