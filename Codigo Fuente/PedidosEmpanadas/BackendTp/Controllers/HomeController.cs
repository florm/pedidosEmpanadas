using BackendTp.Models;
using BackendTp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackendTp.Controllers
{
    public class HomeController : Controller
    {
        ServicioUsuario ServicioUsuario = new ServicioUsuario();

        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Registro(Usuario usuario)
        {
            //ServicioUsuario.CrearUsuario(usuario);
            return Json("");
        }

    }
}