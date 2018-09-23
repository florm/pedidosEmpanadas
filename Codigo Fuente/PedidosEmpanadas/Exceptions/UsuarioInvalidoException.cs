using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class UsuarioInvalidoException : Exception
    {
        public override string Message => "Verifique nombre de usuario y contraseña. Login incorrecto";
    }
}
