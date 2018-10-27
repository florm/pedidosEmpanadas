using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class GustosPedidoUsuarioViewModel : PedidoGustosEmpanadasViewModel
    {
        public List<InvitacionPedidoGustoEmpanadaUsuario> GustosElegidosUsuario { get; set; }
        public InvitacionPedido InvitacionPedido { get; set; }
    }
}