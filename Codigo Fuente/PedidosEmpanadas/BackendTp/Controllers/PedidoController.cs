using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendTp.Models;
using BackendTp.Servicios;

namespace BackendTp.Controllers
{
    public class PedidoController : BaseController
    {
        private readonly ServicioPedido _servicioPedido = new ServicioPedido();
        private readonly ServicioGustoEmpanada _servicioGustoEmpanada = new ServicioGustoEmpanada();
        private readonly ServicioInvitacionPedido _servicioInvitacionPedido = new ServicioInvitacionPedido();
        private readonly ServicioUsuario _servicioUsuario = new ServicioUsuario();


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
            pedidoGustosEmpanadas.Invitados = _servicioUsuario.GetAllByEmail(pedidoGustosEmpanadas.Invitados);
            ViewBag.iniciar = false;
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

        public ActionResult Lista()
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
            return View("Editar", pgeVM);
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
            //var invi = _servicioInvitacionPedido.GetInvitacionGustosPedidoPorPedido(id, usuarioId);
            //var invitacionPedido = _servicioInvitacionPedido.GetInvitacionPedidoPorPedido(id, usuarioId);
            //return View(invi);


            PedidoGustosEmpanadasViewModel pgeVm = new PedidoGustosEmpanadasViewModel();
            var gustos = _servicioGustoEmpanada.GetGustosEnPedido(id);
            pgeVm.Pedido = _servicioPedido.GetById(id);
            pgeVm.GustosElegidosUsuario = _servicioGustoEmpanada.GetGustosDeUsuario(id, usuarioId);
            foreach (var gusto in gustos)
            {
                pgeVm.GustosDisponibles.Add(new GustosEmpanadasViewModel(gusto.IdGustoEmpanada, gusto.Nombre));

            }
            ViewBag.elegirPrimero = true;
            return View(pgeVm);
        }

        public ActionResult Modificar(PedidoGustosEmpanadasViewModel pedidoGustosEmpanadas)
        {
            //todo Esto es de crear. Hacer logica para modificacion del pedido
            //if (ModelState.IsValid)
            //{
            //    var pedidoNuevo = _servicioPedido.Crear(pedidoGustosEmpanadas);
            //    _servicioInvitacionPedido.Crear(pedidoNuevo, pedidoGustosEmpanadas.Invitados);
            //    return RedirectToAction("Iniciado", new { id = pedidoNuevo.IdPedido });

            //}
            pedidoGustosEmpanadas.Invitados = _servicioUsuario.GetAllByEmail(pedidoGustosEmpanadas.Invitados);
            ViewBag.iniciar = false;
            return View("Editar", pedidoGustosEmpanadas);
        }
    }
}