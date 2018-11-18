using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BackendTp.Enums;
using BackendTp.Models;
using BackendTp.Servicios;
using BackendTp.Helpers;
using Exceptions;
using PagedList;

namespace BackendTp.Controllers
{
    public class PedidoController : BaseController
    {
        private readonly ServicioPedido _servicioPedido = new ServicioPedido();
        private readonly ServicioGustoEmpanada _servicioGustoEmpanada = new ServicioGustoEmpanada();
        private readonly ServicioInvitacionPedido _servicioInvitacionPedido = new ServicioInvitacionPedido();
        private readonly ServicioUsuario _servicioUsuario = new ServicioUsuario();
        private readonly ServicioEmail _servicioEmail= new ServicioEmail();

        public ActionResult Iniciar()
        {
            PedidoGustosEmpanadasViewModel pgeVm = new PedidoGustosEmpanadasViewModel();
            var gustos = _servicioGustoEmpanada.GetAll();
            foreach (var gusto in gustos)
            {
                pgeVm.GustosDisponibles.Add(new GustosEmpanadasViewModel(gusto.IdGustoEmpanada, gusto.Nombre));

            }
            ViewBag.iniciar = true;
            return View(pgeVm);
        }

        public ActionResult Crear(PedidoGustosEmpanadasViewModel pedidoGustosEmpanadas)
        {
            if (ModelState.IsValid && pedidoGustosEmpanadas.Invitados.Count != 0)
            {
                var pedidoNuevo = _servicioPedido.Crear(pedidoGustosEmpanadas);
                var usuarios = _servicioInvitacionPedido.Crear(pedidoNuevo, pedidoGustosEmpanadas.Invitados, Sesion.IdUsuario);
                _servicioEmail.ArmarMailInicioPedido(usuarios, pedidoNuevo.IdPedido);
                return RedirectToAction("Iniciado", new { id = pedidoNuevo.IdPedido });

            }
            pedidoGustosEmpanadas.Invitados = _servicioUsuario.GetAllByEmail(pedidoGustosEmpanadas.Invitados);
            if(pedidoGustosEmpanadas.Invitados.Count == 0)
                ViewBag.mensajeError = "Debe seleccionar al menos un invitado";
            ViewBag.iniciar = false;
            return View("Iniciar", pedidoGustosEmpanadas);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Iniciado(int id)
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Elegir(Guid id)
        {
            var gpu = _servicioPedido.ElegirGustosPorToken(id);
            return View("ElegirGustos", gpu);
        }

        //public ActionResult ListaPedidosTotal()
        //{
        //    var pedidos = _servicioPedido.GetAll();
        //    return View(pedidos);
        //}

        [System.Web.Mvc.HttpGet]
        public ActionResult Editar(int id)
        {
            _servicioUsuario.ValidarPermisoUsuario(id, Sesion.IdUsuario);
            var pedido = _servicioPedido.GetById(id);
            //if (pedido.IdEstadoPedido == (int)EstadosPedido.Cerrado)
            //    throw new PedidoCerradoException();
            if (pedido.EstadoPedido.IdEstadoPedido == (int) EstadosPedido.Cerrado)
            {
                return RedirectToAction("Detalle", new { id });
            }
            var gustosModel = _servicioGustoEmpanada.GetAll();
            var invitados = _servicioInvitacionPedido.GetByIdPedido(pedido, Sesion.IdUsuario);
            var pgeVM = new PedidoGustosEmpanadasViewModel(pedido, pedido.GustoEmpanada.ToList(), 
                gustosModel, invitados);
            ViewBag.iniciar = false;
            ViewBag.emailAcciones = _servicioEmail.GetAcciones();
            return View("Editar", pgeVM);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult ElegirGustos(int id, int usuarioId)
        {
            var gpu = _servicioPedido.ElegirGustosUsuario(id, usuarioId);

            return View(gpu);
        }

        public ActionResult Lista(int? pag)
        {
            List<Pedido> pedidos = new List<Pedido>();
            pedidos = _servicioPedido.Lista(Sesion.IdUsuario);
            return View(pedidos.ToPagedList(pag ?? 1, 10));
        }

        public ActionResult Modificar(PedidoGustosEmpanadasViewModel pedidoGustosEmpanadas)
        {
            if (ModelState.IsValid)
            {
                var pedidoId = _servicioPedido.Modificar(pedidoGustosEmpanadas);
                _servicioInvitacionPedido.Modificar(pedidoId, pedidoGustosEmpanadas.Invitados, pedidoGustosEmpanadas.Acciones);
                return RedirectToAction("Lista");

            }
            pedidoGustosEmpanadas.Invitados = _servicioUsuario.GetAllByEmail(pedidoGustosEmpanadas.Invitados);
            ViewBag.iniciar = false;
            ViewBag.emailAcciones = _servicioEmail.GetAcciones();
            return View("Editar", pedidoGustosEmpanadas);
        }

        public ActionResult Confirmar([FromBody] Pedido pedido)
        {
            _servicioPedido.Confirmar(pedido);
            _servicioEmail.ArmarMailsConfirmacion(pedido);
            return RedirectToAction("Lista", "Pedido");
        }

        
        public JsonResult DetallesPedido([FromBody]PedidoViewModel pvm)
        {
            var pedido = _servicioPedido.GetById(pvm.IdPedido);
            var pedidoAEliminar = new
            {
                NombreNegocio = pedido.NombreNegocio,
                Cantidad = pedido.InvitacionPedido.Count(p=>p.Completado == true)
            };
            return Json(pedidoAEliminar);
        }

        [System.Web.Mvc.HttpPost]
        public void Eliminar(int id)
        {
            _servicioPedido.Eliminar(id);   
        }
        
        public ActionResult Detalle(int id)
        {
            Pedido pedidoElegido = _servicioPedido.GetById(id);
            Pedido pedido = _servicioPedido.Detalle(pedidoElegido);
            return View(pedido);
        }

        public ActionResult Clonar(int id)
        {
            PedidoGustosEmpanadasViewModel pedido = _servicioPedido.Clonar(id);
            ViewBag.iniciar = true;
            return View("Iniciar", pedido);
        }
    }
}