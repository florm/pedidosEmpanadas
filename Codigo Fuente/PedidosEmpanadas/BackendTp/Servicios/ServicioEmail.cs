using BackendTp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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


        public void ArmarMailsConfirmacion(EmailViewModel emailViewModel)
        {

            var pedido = Db.Pedido.FirstOrDefault(p => p.IdPedido == emailViewModel.IdPedido);
            EmailInvitados(pedido);
            //EmailResponsable(pedido);


                //esto podria ser para el responsable.. TODO
            //foreach (var i in pedido.InvitacionPedido.)
            //{
            //    var email = new Mail();
            //    email.Email = i.Usuario.Email;

            //    var gustosSeleccionados = i.Pedido.InvitacionPedidoGustoEmpanadaUsuario;

            //    foreach (var gustoDelPedido in gustosDelPedido)
            //    {
            //        var gustoSelecccionado =
            //            gustosSeleccionados.FirstOrDefault(g => g.IdGustoEmpanada == gustoDelPedido.IdGustoEmpanada);

            //        if (gustoSelecccionado != null)
            //        {
            //            email.GustoEmpanadas.Add(new GustoEmpanadaMail
            //            {
            //                GustoEmpanada = gustoDelPedido,
            //                Cantidad = gustoSelecccionado.Cantidad
            //            });
            //        }
            //        else
            //        {
            //            email.GustoEmpanadas.Add(new GustoEmpanadaMail
            //            {
            //                GustoEmpanada = gustoDelPedido,
            //                Cantidad = 0
            //            });
            //        }

            //    }

            //}

            //List<string> emails = new List<string>();
            //var x = invitacionPedidoGustoUsuario.ToList();
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





        }

        private void EmailResponsable(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        private static void EmailInvitados(Pedido pedido)
        {
            //email invitados.
            foreach (var invitado in pedido.InvitacionPedido)
            {
                var email = new Mail();
                email.Email = invitado.Usuario.Email;
                email.PrecioTotal = pedido.PrecioTotal; 
                email.GustosEmpanadas = invitado.Pedido.InvitacionPedidoGustoEmpanadaUsuario.Where(u => u.IdUsuario == invitado.IdUsuario)
                        .Select(g => new GustoEmpanada
                        {
                            Nombre = g.GustoEmpanada.Nombre,
                            Cantidad = g.Cantidad
                        }).ToList();

                MandarMail(email, "Detalles de Pedido Confirmado","invitado" );
            }
        }

        public static void MandarMail(Mail mailInfo, string subject, string tipoUsuario)
        {
            //List<string> invitados = new List<string>()
            //{
            //    "florenciamartin05@gmail.com"
            //};


            //
            // se crea el mensaje
            //
            string mensaje = ArmarMensajeBody(tipoUsuario, mailInfo);

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("pedidosempanadas2018@gmail.com"),
                Body = mensaje,
                Subject = subject,
                IsBodyHtml = true
            };


            //
            // se asignan los destinatarios
            //
            //foreach (string item in invitados)
            //{
            //    mail.To.Add(new MailAddress(item));
            //}
            mail.To.Add(new MailAddress(mailInfo.Email));


            //
            // se define el smtp
            //
            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("pedidosempanadas2018@gmail.com", "1234Flor"),
                EnableSsl = true
            };


            smtp.Send(mail);

        }

        private static string ArmarMensajeBody(string tipoUsuario, Mail mailInfo)
        {
            var mensaje = "";
            switch (tipoUsuario.ToLower())
            {
                case "responsable":
                    mensaje = "";

                    break;
                case "invitado":
                    mensaje = $"Hola {mailInfo.Email}, <br/><br/>" +
                              "Estos son los detalles de tu pedido: <br/>";
                    var cantidadTotal = 0;
                    foreach (var gusto in mailInfo.GustosEmpanadas)
                    {
                        mensaje += "<strong>" + gusto.Nombre + "</strong>" + ": " + gusto.Cantidad + "<br/>";
                        cantidadTotal += gusto.Cantidad;
                    }

                    mensaje +=
                        $"<br/><br/> La <strong>cantidad total</strong> de empanadas que elegiste es: {cantidadTotal}" +
                        "<br/>";
                    mensaje += $"El <strong>precio</strong> que debes abonar es de: {mailInfo.PrecioTotal}" +
                               "<br/>" +
                               "Gracias por elegirnos!";
                    break;
            }

            return mensaje;
        }

    }
}