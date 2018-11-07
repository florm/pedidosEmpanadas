using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int Rol { get; set; }
        public string RolS { get; set; }
        public string EstadoS { get; set; }
        public string NombreNegocio { get; set; }
        public int Estado { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    }
}