using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendTp.Enums;
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
            if (ModelState.IsValid)
            {
                var pedidoNuevo = _servicioPedido.Crear(pedidoGustosEmpanadas);
                _servicioInvitacionPedido.Crear(pedidoNuevo, pedidoGustosEmpanadas.Invitados, Sesion.IdUsuario);
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

        public ActionResult ListaPedidosTotal()
        {
            var pedidos = _servicioPedido.GetAll();
            return View(pedidos);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {

            var pedido = _servicioPedido.GetById(id);
            if (pedido.EstadoPedido.IdEstadoPedido == (int) EstadosPedido.Cerrado)
            {
                //todo
                //RedirectToAction("Detalle");
            }
            var gustosModel = _servicioGustoEmpanada.GetAll();
            var invitados = _servicioInvitacionPedido.GetByIdPedido(pedido, Sesion.IdUsuario);
            var pgeVM = new PedidoGustosEmpanadasViewModel(pedido, pedido.GustoEmpanada.ToList(), 
                gustosModel, invitados);
            ViewBag.iniciar = false;
            ViewBag.emailAcciones = _servicioEmail.GetAcciones();
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


            GustosPedidoUsuarioViewModel gpu = new GustosPedidoUsuarioViewModel();
            var gustos = _servicioGustoEmpanada.GetGustosEnPedido(id);
            gpu.Pedido = _servicioPedido.GetById(id);
            gpu.InvitacionPedido = _servicioPedido.GetInvitacion(id, usuarioId);
            gpu.GustosElegidosUsuario = _servicioGustoEmpanada.GetGustosDeUsuario(id, usuarioId);
            foreach (var gusto in gustos)
            {
                gpu.GustosDisponibles.Add(new GustosEmpanadasViewModel(gusto.IdGustoEmpanada, gusto.Nombre));

            }

            if(gpu.GustosDisponibles.Count() > gpu.GustosElegidosUsuario.Count())
            {
                foreach(InvitacionPedidoGustoEmpanadaUsuario gu in gpu.GustosElegidosUsuario) { 
                    foreach(GustosEmpanadasViewModel g in gpu.GustosDisponibles)
                    {
                        if(gu.IdGustoEmpanada == g.Id)
                        {
                            g.IsSelected = true;
                        }
                    }
                }
            }

            ViewBag.elegirPrimero = true;
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
            //todo Esto es de crear. Hacer logica para modificacion del pedido
            //if (ModelState.IsValid)
            //{
            //    var pedidoNuevo = _servicioPedido.Crear(pedidoGustosEmpanadas);
            //    _servicioInvitacionPedido.Crear(pedidoNuevo, pedidoGustosEmpanadas.Invitados);
            //    return RedirectToAction("Iniciado", new { id = pedidoNuevo.IdPedido });

            //}
            pedidoGustosEmpanadas.Invitados = _servicioUsuario.GetAllByEmail(pedidoGustosEmpanadas.Invitados);
            ViewBag.iniciar = false;
            ViewBag.emailAcciones = _servicioEmail.GetAcciones();
            return View("Editar", pedidoGustosEmpanadas);
        }
    }
}