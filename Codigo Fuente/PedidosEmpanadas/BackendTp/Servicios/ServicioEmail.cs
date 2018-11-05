using BackendTp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackendTp.Models;

namespace BackendTp.Servicios
{
    public class ServicioEmail : Servicio
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


        public void ArmarMail(EmailViewModel emailViewModel)
        {
            var invitacionPedidoGustoUsuario = Db.InvitacionPedidoGustoEmpanadaUsuario
                .Where(ip => ip.IdPedido == emailViewModel.IdPedido)
                .GroupBy(i=>i.IdUsuario)
                .ToList();
            
            




            List<string> emails = new List<string>();
            //switch (emailViewModel.Acciones)
            //{
            //    case (int)EmailAcciones.ReEnviarInvitacionATodos:
            //        emails = invitados.Select(i => i.Email).ToList();
            //        break;
            //    case (int)EmailAcciones.EnviarSoloALosNuevos:
            //        emails = emailViewModel.NuevosInvitados;
            //        break;
            //    case (int)EmailAcciones.ReEnviarSoloALosQueNoEligieronGustos:
            //        emails = Db.InvitacionPedido
            //            .Where(ip => ip.IdPedido == emailViewModel.IdPedido && ip.Completado == false)
            //            .Select(u => u.Usuario.Email).ToList();
            //        break;
            //    default:
            //        emails = null;
            //        break;
                        
            //}

            if(emails != null)
                MailingHelper.MandarMail("hola", emails, "prueba");
            
                

        }

    }
}