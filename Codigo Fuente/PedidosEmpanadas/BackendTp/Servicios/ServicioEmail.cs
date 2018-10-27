using BackendTp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Servicios
{
    public class ServicioEmail
    {
        
        public List<EmailAccion> GetAcciones()
        {
            return new List<EmailAccion>
            {
                new EmailAccion{ Id = 1, Nombre = "A Nadie"},
                new EmailAccion{ Id = 2, Nombre = "Re-enviar Invitación a Todos"},
                new EmailAccion{ Id = 3, Nombre ="Enviar sólo a los Nuevos"},
                new EmailAccion{ Id = 4, Nombre = "Re-enviar sólo a los que no eligieron gustos"}
            };
        }

    }
}