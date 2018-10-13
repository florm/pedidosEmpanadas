using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class PedidoGustosEmpanadasViewModel
    {
        public Pedido Pedido { get; set; }
        public List<GustosEmpanadasViewModel> GustosDisponibles{ get; set; }

        public PedidoGustosEmpanadasViewModel()
        {
            GustosDisponibles = new List<GustosEmpanadasViewModel>();
        }

    }
}