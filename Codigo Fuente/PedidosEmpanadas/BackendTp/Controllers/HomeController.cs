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
        private readonly ServicioUsuario _servicioUsuario = new ServicioUsuario();
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Registro(Usuario usuario)
        {
            _servicioUsuario.CrearUsuario(usuario);
            return Json("");
        }


        public JsonResult Login(Usuario usuario)
        {
            var usuarioLogueado = _servicioUsuario.Login(usuario);
            Sesion.IdUsuario = usuarioLogueado.IdUsuario;
            Sesion.EmailUsuario = usuarioLogueado.Email;
            return Json("");
        }

        public ActionResult LogOut()
        {
            Sesion.IdUsuario = 0;
            return RedirectToAction("Index");
        }




    }
}