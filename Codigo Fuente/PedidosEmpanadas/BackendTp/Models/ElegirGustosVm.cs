using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class ElegirGustosVm
    {
        public List<GustoEmpanadasCantidad> Empanadas { get; set; }
        public Pedido Pedido { get; set; }
        public System.Guid Token { get; set; }
    }
}