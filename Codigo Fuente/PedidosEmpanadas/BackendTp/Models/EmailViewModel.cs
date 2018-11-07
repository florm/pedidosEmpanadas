using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class EmailViewModel
    {
        public int IdPedido { get; set; }
        public int Acciones { get; set; }
        public List<string> NuevosInvitados { get; set; }
    }
}