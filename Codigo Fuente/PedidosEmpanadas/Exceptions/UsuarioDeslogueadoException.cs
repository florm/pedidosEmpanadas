using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Exceptions
{
    public class UsuarioDeslogueadoException : RedireccionamientoException
    {
        public UsuarioDeslogueadoException()
        {
            PathRedirect = "~/Home/Index";
        }

        public override string Message => "No se ha iniciado sesion.";
    }
}
