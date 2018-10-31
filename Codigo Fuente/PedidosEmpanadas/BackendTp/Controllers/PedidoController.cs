using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendTp.Models;
using BackendTp.Servicios;
using BackendTp.Helpers;
using PagedList;

namespace BackendTp.Controllers
{
    public class PedidoController : BaseController
    {
        private readonly ServicioPedido _servicioPedido = new ServicioPedido();
        private readonly ServicioGustoEmpanada _servicioGustoEmpanada = new ServicioGustoEmpanada();
        private readonly ServicioInvitacionPedido _servicioInvitacionPedido = new ServicioInvitacionPedido();


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
            if (ModelState.IsValid)
            {
                var pedidoNuevo = _servicioPedido.Crear(pedidoGustosEmpanadas);
                _servicioInvitacionPedido.Crear(pedidoNuevo, pedidoGustosEmpanadas.Invitados);
                return RedirectToAction("Iniciado", new { id = pedidoNuevo.IdPedido });

            }

            ViewBag.iniciar = true;
            return View("Iniciar", pedidoGustosEmpanadas);
        }

        [HttpGet]
        public ActionResult Iniciado(int id)
        {
            return View();
        }
        public ActionResult Elegir()
        {
            var gustos = _servicioGustoEmpanada.GetAll();
            return View(gustos);
        }

        public ActionResult ListaPedidosTotal()
        {
            var pedidos = _servicioPedido.GetAll();
            return View(pedidos);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var pedido = _servicioPedido.GetById(id);
            var gustosModel = _servicioGustoEmpanada.GetAll();
            var invitados = _servicioInvitacionPedido.GetByIdPedido(pedido);
            var pgeVM = new PedidoGustosEmpanadasViewModel(pedido, pedido.GustoEmpanada.ToList(), 
                gustosModel, invitados);
            ViewBag.iniciar = false;
            return View("Iniciar", pgeVM);
        }

        //[Route("ElegirGustos/{id}")]
        //public ActionResult ElegirGustos(int id, int usuarioId)
        //{
        //    var invitacionPedido = _servicioInvitacionPedido.GetInvitacionPedidoPorPedido(id);
        //    return View();
        //}

        [HttpGet]
        public ActionResult ElegirGustos(int id, int usuarioId)
        {
            var invitacionPedido = _servicioInvitacionPedido.GetInvitacionPedidoPorPedido(id, usuarioId);
            return View(invitacionPedido);
        }

        public ActionResult Lista(int? pag)
        {
            List<Pedido> pedidos = new List<Pedido>();
            pedidos = _servicioPedido.Lista(Sesion.IdUsuario);
            return View(pedidos.ToPagedList(pag ?? 1, 4));
        }

        public ActionResult Detalle(int id)
        {
            Pedido pedidoElegido = _servicioPedido.GetById(id);
            Pedido pedido = _servicioPedido.Detalle(pedidoElegido);
            return View(pedido);
        }
    }
}