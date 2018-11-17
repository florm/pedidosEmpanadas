using BackendTp.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using BackendTp.Models;
using WebGrease.Css.Extensions;

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


        public void ArmarMailsConfirmacion(Pedido pedidoView)
        {

            var pedido = Db.Pedido.FirstOrDefault(p => p.IdPedido == pedidoView.IdPedido);
            EmailInvitados(pedido);
            EmailResponsable(pedido);

        }

        private void EmailResponsable(Pedido pedido)
        {
            var email = new Mail();
            email.Email = pedido.Usuario.Email; //usuario responsable
            email.PrecioTotal = pedido.PrecioTotal;

            //armos los gustos, seleccionados y no seleccionados para tener el detalle a enviar el responsable
            var gustosSeleccionados = pedido.InvitacionPedidoGustoEmpanadaUsuario;

            foreach (var gustoDelPedido in pedido.GustoEmpanada)
            {
                var gustoSelecccionado =
                    gustosSeleccionados
                        .FirstOrDefault(g => g.IdGustoEmpanada == gustoDelPedido.IdGustoEmpanada);

                if (gustoSelecccionado != null)
                {
                    var cantidadTotal = 0;
                    gustosSeleccionados.Where(gs=>gs.IdGustoEmpanada == gustoSelecccionado.IdGustoEmpanada)
                        .ForEach(gs => { cantidadTotal += gs.Cantidad; });
                    email.GustosEmpanadas.Add(new GustoEmpanada
                    {
                        Nombre = gustoSelecccionado.GustoEmpanada.Nombre,
                        Cantidad = cantidadTotal
                    });
                }
                else
                {
                    email.GustosEmpanadas.Add(new GustoEmpanada
                    {
                        Nombre = gustoDelPedido.Nombre,
                        Cantidad = 0
                    });
                }

            }

            //armo la lista de invitados con su detalle de mail y precio total que paga cada uno
            foreach (var i in pedido.InvitacionPedido)
            {
                var cantidad = 0;
                var pedidosDeUsuario = i.Pedido.InvitacionPedidoGustoEmpanadaUsuario.Where(u => u.IdUsuario == i.IdUsuario).ToList();
                if (pedidosDeUsuario.Count() != 0)
                {
                    foreach (var pu in pedidosDeUsuario)
                    {
                        cantidad += pu.Cantidad;
                    }
                    email.InvitadosMail.Add(new InvitadosMail {Email = i.Usuario.Email, PrecioTotal = pedido.PrecioPorUnidad * cantidad}); 

                }

            }

            MandarMail(email, "Detalle de Pedido Confirmado", "responsable");
        }

        private static void EmailInvitados(Pedido pedido)
        {
            //email invitados.
            foreach (var invitado in pedido.InvitacionPedido)
            {
                var email = new Mail();
                email.Email = invitado.Usuario.Email;
                email.GustosEmpanadas = invitado.Pedido.InvitacionPedidoGustoEmpanadaUsuario.Where(u => u.IdUsuario == invitado.IdUsuario)
                        .Select(g => new GustoEmpanada
                        {
                            Nombre = g.GustoEmpanada.Nombre,
                            Cantidad = g.Cantidad
                        }).ToList();
                var cantidadTotal=0;
                email.GustosEmpanadas.ForEach(ge => { cantidadTotal += ge.Cantidad; });
                email.PrecioTotal = pedido.PrecioPorUnidad * cantidadTotal; 
                MandarMail(email, "Detalles de Pedido Confirmado","invitado" );
            }
        }

        public static void MandarMail(Mail mailInfo, string subject, string tipoMail)
        {
            string mensaje = ArmarMensajeBody(tipoMail, mailInfo);

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("pedidosempanadas2018@gmail.com"),
                Body = mensaje,
                Subject = subject,
                IsBodyHtml = true
            };

            mail.To.Add(new MailAddress(mailInfo.Email));
            
            // se define el smtp
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

        private static string ArmarMensajeBody(string tipoMail, Mail mailInfo)
        {
            var mensaje = "";
            switch (tipoMail.ToLower())
            {
                case "responsable":
                    mensaje = $"Hola {mailInfo.Email}, <br/><br/>" +
                              "Estos son los detalles de tu pedido: <br/>";
                    mensaje += "<strong>Detalle de la recaudación:</strong> <br/>" +
                               "Precio Total: $" + mailInfo.PrecioTotal + "<br/>" +
                               "Invitados: <br/>";
                    foreach (var invitado in mailInfo.InvitadosMail)
                    {
                        mensaje += invitado.Email + ": $" + invitado.PrecioTotal + "<br/>";
                    }

                    mensaje += "<strong>Detalle del pedido:</strong> <br/>" +
                               "Gustos: <br/>";
                    foreach (var gusto in mailInfo.GustosEmpanadas)
                    {
                        mensaje += gusto.Nombre+ ": " + gusto.Cantidad+ "<br/>";
                    }

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
                    mensaje += $"El <strong>precio</strong> que debes abonar es de: ${mailInfo.PrecioTotal}" +
                               "<br/>" +
                               "Gracias por elegirnos!";
                    break;
                case "inicio":
                    mensaje = $"Hola {mailInfo.Email}, <br/><br/>" +
                              "Se ha iniciado un nuevo pedido de empanadas<br/>" +
                              "Podes seleccionar los gustos que desees en el siguiente " +
                              "<a href='" + mailInfo.Link + "'>link</a>";
                    break;
            }

            return mensaje;
        }

        public void ArmarMailInicioPedido(List<int> usuarios, int idPedido)
        {
            foreach (var u in usuarios)
            {
                var email = new Mail();
                var tokenUsuario = Db.InvitacionPedido
                    .FirstOrDefault(ip => ip.IdUsuario == u && ip.IdPedido == idPedido)?.Token;
                email.Link = "http://" + HttpContext.Current.Request.Url.Authority + "/pedido/elegir/" + tokenUsuario;
                email.Email = Db.Usuario.FirstOrDefault(um => um.IdUsuario == u)?.Email;
                MandarMail(email, "Inicio de Pedido", "inicio");
            }
        }
    }
}