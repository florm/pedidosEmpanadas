using BackendTp.Models;
using BackendTp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BackendTp.Controllers
{
    public class UsuariosController : ApiController
    {
        ServicioUsuario servicioUsuario = new ServicioUsuario();
        ServicioPedido servicioPedido = new ServicioPedido();

        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetMobileUser(string id, string param2)
        {
            var usuario = servicioUsuario.LoginMobile(id, param2);
            return Ok(usuario);
        }

        [ResponseType(typeof(List<PedidoViewModel>))]
        public IHttpActionResult GetListaPedidos(int id)
        {
            List<PedidoViewModel> ListaPedido = servicioPedido.ListarPedidosMobile(id);
            return Ok(ListaPedido);
        }
    }
}
