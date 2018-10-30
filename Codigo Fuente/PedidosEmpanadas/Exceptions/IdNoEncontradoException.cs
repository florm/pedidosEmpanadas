using Exceptions.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
    public class IdNoEncontradoException : Exception
    {
        public string Nombre { get; set; }
        public string GeneroEntidad { get; set; }

        public IdNoEncontradoException(Genero generoEntidad, string nombreModeloOEntidad)
        {
            Nombre = nombreModeloOEntidad;
            switch (generoEntidad)
            {
                case Genero.Femenino:
                    GeneroEntidad = "la";
                    break;
                case Genero.Masculino:
                    GeneroEntidad = "el";
                    break;

            }
        }
        public override string Message => $"No se he encontrado {GeneroEntidad} {Nombre}";
    }
}
