using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class PedidoRequest
    {
        public List<GustoEmpanadasCantidad> GustoEmpanadasCantidad { get; set; }
        public int IdUsuario { get; set; }
        public int IdPedido { get; set; }
    }
}
