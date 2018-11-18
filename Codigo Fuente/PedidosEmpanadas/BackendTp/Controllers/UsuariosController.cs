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
        private static readonly Entities Context = new Entities();

        private readonly ServicioUsuario servicioUsuario = new ServicioUsuario(Context);
        private readonly ServicioPedido servicioPedido = new ServicioPedido(Context);

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

        [ResponseType(typeof(string))]
        public IHttpActionResult PostNewUser(Usuario usuario)
        {
            var valida = servicioUsuario.CrearUsuario(usuario);
            string respuesta = valida.Item1;
            return Ok(respuesta);
        }

        [ResponseType(typeof(List<PedidoViewModel>))]
        public IHttpActionResult GetListaDeGustosEnPedido(int id, int idUsuario)
        {
            List<PedidoViewModel> ListaPedido = servicioPedido.ListarPedidosMobile(id);
            return Ok(ListaPedido);
        }
    }
}
