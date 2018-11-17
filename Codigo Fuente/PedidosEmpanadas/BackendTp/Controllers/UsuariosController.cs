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
            List<PedidoViewModel> ListaPrueba = new List<PedidoViewModel>();
            var pedidos = servicioPedido.Lista(id);

            foreach (Pedido p in pedidos)
            {
                ListaPrueba.Add(new PedidoViewModel
                {
                    IdPedido = p.IdPedido,
                    IdUsuarioResponsable = p.IdUsuarioResponsable,
                    FechaCreacion = p.FechaCreacion,
                    NombreNegocio = p.NombreNegocio,
                    Estado = p.IdEstadoPedido,
                    Rol = p.IdUsuarioResponsable,
                    EstadoS = p.EstadoPedido.Nombre
                });
            }

            foreach (PedidoViewModel pvm in ListaPrueba)
            {
                if (pvm.Rol == id)
                {
                    pvm.RolS = "Responsable";
                }
                else
                {
                    pvm.RolS = "Invitado";
                }

            }

            return Ok(ListaPrueba);
        }
    }
}
