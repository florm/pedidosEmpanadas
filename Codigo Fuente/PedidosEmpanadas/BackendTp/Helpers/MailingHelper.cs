using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Web;
using System.Web.Services.Description;
//using MimeKit;

namespace BackendTp.Helpers
{
    public class MailingHelper
    {
        public static void MandarMail(string body, List<string> destinatarios, string subject)
        {
            List<string> invitados = new List<string>()
            {
                "florenciamartin05@gmail.com"
            };


            //
            // se crea el mensaje
            //
            string mensaje = "hola";

            
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("pedidosempanadas2018@gmail.com"),
                Body = mensaje,
                Subject = subject,
                IsBodyHtml = false
            };


            //
            // se asignan los destinatarios
            //
            foreach (string item in invitados)
            {
                mail.To.Add(new MailAddress(item));
            }


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


    }
}