using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class PedidosVm
    {
        public int IdPedido { get; set; }
        public int IdUsuarioResponsable { get; set; }
        public int Rol { get; set; }
        public string NombreNegocio { get; set; }
        public int Estado { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string RolS { get; set; }
        public string EstadoS { get; set; }
    }
}
