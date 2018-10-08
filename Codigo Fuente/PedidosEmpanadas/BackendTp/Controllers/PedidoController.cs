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

        public ActionResult Iniciar()
        {
            ViewBag.Gustos = _servicioGustoEmpanada.GetAll();
            return View();
        }

        public ActionResult Crear(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                var pedidoNuevo = _servicioPedido.Crear(pedido);
                return RedirectToAction("Iniciado", new { id = pedidoNuevo.IdPedido });
            }

            return View("Iniciar", pedido);
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
    }
}