using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class Mail
    {
        public string Email { get; set; }
        public List<GustoEmpanada> GustosEmpanadas { get; set; }
        public List<InvitadosMail> InvitadosMail { get; set; }
        public int CantidadTotal { get; set; }
        public decimal PrecioTotal { get; set; }
        public string Link { get; set; }

        public Mail()
        {
            GustosEmpanadas = new List<GustoEmpanada>();
            InvitadosMail = new List<InvitadosMail>();
        }
    }

    
}