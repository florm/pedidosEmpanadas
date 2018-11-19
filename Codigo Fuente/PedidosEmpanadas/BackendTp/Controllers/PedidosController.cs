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
using System.Threading.Tasks;

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

        [ResponseType(typeof(List<PedidoViewModel>))]
        public IHttpActionResult GetListaPedidos(int id)
        {
            List<PedidoViewModel> ListaPedido = servicioPedido.ListarPedidosMobile(id);
            return Ok(ListaPedido);
        }

        [ResponseType(typeof(List<GustoEmpanadasCantidad>))]
        public IHttpActionResult GetListaDeGustosEnPedido(int id, int param2)
        {
            var gpu = servicioPedido.GetGustosForMobile(id, param2);
            return Ok(gpu);
        }

    }
}
