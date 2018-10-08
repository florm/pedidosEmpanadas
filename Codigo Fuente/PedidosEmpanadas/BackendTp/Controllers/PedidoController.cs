using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendTp.Servicios;

namespace BackendTp.Controllers
{
    public class PedidoController : BaseController
    {
        ServicioPedido ServicioPedido = new ServicioPedido();
        public ActionResult Pedido()
        {
            return View();
        }

        public ActionResult Elegir()
        {
            var gustos = ServicioPedido.GetAll();
            return View(gustos);
        }
    }
}