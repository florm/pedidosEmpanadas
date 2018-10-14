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


        public ActionResult Iniciar()
        {
            PedidoGustosEmpanadasViewModel pgeVm = new PedidoGustosEmpanadasViewModel();
            var gustos = _servicioGustoEmpanada.GetAll();
            foreach (var gusto in gustos)
            {
                pgeVm.GustosDisponibles.Add(new GustosEmpanadasViewModel(gusto.IdGustoEmpanada, gusto.Nombre));

            }
            return View(pgeVm);
        }

        public ActionResult Crear(PedidoGustosEmpanadasViewModel pedidoGustosEmpanadas, FormCollection invitados)
        {
            if (ModelState.IsValid)
            {
                var pedidoNuevo = _servicioPedido.Crear(pedidoGustosEmpanadas);
                _servicioInvitacionPedido.Crear(pedidoNuevo, Request.Form.GetValues("invitados"));
                return RedirectToAction("Iniciado", new { id = pedidoNuevo.IdPedido });

            }


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
            var pgeVM = new PedidoGustosEmpanadasViewModel(pedido, pedido.GustoEmpanada.ToList(), gustosModel);
            
            return View("Iniciar", pgeVM);
        }
    }
}