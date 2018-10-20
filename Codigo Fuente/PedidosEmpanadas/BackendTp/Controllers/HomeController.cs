using BackendTp.Models;
using BackendTp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendTp.Helpers;

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
            ServicioUsuario.CrearUsuario(usuario);
            return Json("");
        }


        public JsonResult LoginOk(Usuario usuario)
        {
            var usuarioLogueado = ServicioUsuario.Login(usuario);
            Sesion.IdUsuario = usuarioLogueado.IdUsuario;
            Sesion.EmailUsuario = usuarioLogueado.Email;
            return Json("");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Sesion.IdUsuario = 0;
            return RedirectToAction("Index");
        }




    }
}