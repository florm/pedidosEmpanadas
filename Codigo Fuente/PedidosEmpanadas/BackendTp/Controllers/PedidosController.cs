using BackendTp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using System.Web.Mvc;
using BackendTp.Models;
using RouteAttribute = System.Web.Http.RouteAttribute;
using System.Web.Http.Description;

namespace BackendTp.Controllers
{
    
    public class PedidosController : ApiController
    {
        private static readonly Entities Context = new Entities();
        private readonly ServicioInvitacionPedido _servicioInvitacionPedido = new ServicioInvitacionPedido(Context);
        private readonly ServicioPedido servicioPedido = new ServicioPedido(Context);

        [ResponseType(typeof(string))]
        public IHttpActionResult ConfirmarGustos(PedidoRequest pedido)
        {
            var resp = _servicioInvitacionPedido.ConfirmarGustos(pedido);
            if (resp == true)
            {
                return Ok("Gustos elegidos satisfactoriamente");
            }
            else
            {
                return Ok("No se pudo efectuar la operación");
            }
        }

        //[ResponseType(typeof(string))]
        //public IHttpActionResult EliminarPedido(int id)
        //{
        //    servicioPedido.Eliminar(id);
        //    return Ok("Pedido eliminado satisfactoriamente.");
        //}

        public void EliminarPedido(int id)
        {
            servicioPedido.Eliminar(id);

        }

        ////[Route("api/pedidos/confirmargustos")]
        //public JsonResult ConfirmarGustos([FromBody] PedidoRequest pedido)
        //{
        //    var resp = _servicioInvitacionPedido.ConfirmarGustos(pedido);
        //    if (resp == true)
        //    {
        //        //return Json(new Respuesta() { Resultado = "OK", Mensaje = "Gustos elegidos satisfactoriamente" });
        //        return new JsonResult
        //        {
        //            Data = new { Resultado = "OK", Mensaje = "Gustos elegidos satisfactoriamente"}
        //        };
        //    }
        //    else
        //    {
        //        //return Json(new Respuesta() { Resultado = "ERROR", Mensaje = "No se pudo efectuar la operación" });
        //        return new JsonResult
        //        {
        //            Data = new { Resultado = "ERROR", Mensaje = "No se pudo efectuar la operación" },
        //        };
        //    }

        //}

    }
}
