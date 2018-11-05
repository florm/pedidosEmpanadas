using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class Mail
    {
        public Mail(string htmlMsg, string subject)
        {
            HtmlMsg = htmlMsg;
            Subject = subject;
        }

        public string HtmlMsg { get; set; }
        public string Subject { get; set; }
    }
}