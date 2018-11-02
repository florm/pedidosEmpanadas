using Api.Models;
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


namespace Api.Controllers
{
  
    public class PedidosController : ApiController
    {
        ////[HttpPost]
        private readonly ServicioInvitacionPedido _servicioInvitacionPedido = new ServicioInvitacionPedido();

        public JsonResult ConfirmarGustos([FromBody] PedidoRequest pedido)
        {
            var resp = _servicioInvitacionPedido.ConfirmarGustos(pedido);
            if (resp == true)
            {
                //return Json(new Respuesta() { Resultado = "OK", Mensaje = "Gustos elegidos satisfactoriamente" });
                return new JsonResult
                {
                    Data = new { Resultado = "OK", Mensaje = "Gustos elegidos satisfactoriamente"}
                };
            }
            else
            {
                //return Json(new Respuesta() { Resultado = "ERROR", Mensaje = "No se pudo efectuar la operación" });
                return new JsonResult
                {
                    Data = new { Resultado = "ERROR", Mensaje = "No se pudo efectuar la operación" },
                };
            }

        }

    }
}
