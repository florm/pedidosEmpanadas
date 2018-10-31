using Api.Models;
using BackendTp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;


namespace Api.Controllers
{
  
    public class PedidosController : ApiController
    {
        ////[HttpPost]
        private readonly ServicioInvitacionPedido _servicioInvitacionPedido = new ServicioInvitacionPedido();

        public JsonResult<PedidoRequest> ConfirmarGustos([FromBody] PedidoRequest pedido)
        {
            var test = _servicioInvitacionPedido.ConfirmarGustos(pedido);

            var a = 5;
            return null;
        }

    }
}
