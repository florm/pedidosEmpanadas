using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Helpers
{
    public enum EmailAcciones
    {
       ANadie = 1,
       ReEnviarInvitacionATodos,
       EnviarSoloALosNuevos,
       ReEnviarSoloALosQueNoEligieronGustos
    }

    public class EmailAccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}