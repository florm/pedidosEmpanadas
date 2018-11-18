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
        private static readonly Entities Context = new Entities();
        private readonly ServicioUsuario _servicioUsuario = new ServicioUsuario(Context);
        
        // GET: Home
        public ActionResult Index()
        {
            return Sesion.IdUsuario != 0 ? (ActionResult) RedirectToAction("Lista","Pedido") : View();
        }

        public JsonResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var (operacion, mensaje) = _servicioUsuario.CrearUsuario(usuario);
                return Json(new
                {
                    mensaje,
                    operacion
                });
            }

            return Json(new
            {
                mensaje="Debe completar mail y contraseña",
                operacion="error"
            });
        }

        public JsonResult LoginOk(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
            var usuarioLogueado = _servicioUsuario.Login(usuario);
            Sesion.IdUsuario = usuarioLogueado.IdUsuario;
            Sesion.EmailUsuario = usuarioLogueado.Email;

            }
            if(Sesion.UltimaUrlAccedida != null)
                return Json(Sesion.UltimaUrlAccedida);
            return Json("");

        }

        public ActionResult Login()
        {
            
            return View();
        }

        public ActionResult LogOut()
        {
            Sesion.IdUsuario = 0;
            Sesion.UltimaUrlAccedida = null;
            return RedirectToAction("Index");
        }

        
        public ActionResult Error(int? error, string mensaje)
        {
            if (error == null)
            {
                ViewBag.code = "505";
            }
            else
            {
                ViewBag.code = error;
            }
                
            switch (error)
            {
                case 505:
                    ViewBag.Title = "ERROR EN EL SERVIDOR";
                    ViewBag.Description = "Ocurrió un error inesperado, esperamos que no vuelva a pasar.";
                    break;
                case 405:
                    ViewBag.Title = "Acción no permitida";
                    ViewBag.Description = mensaje;
                    break;
                default:
                    ViewBag.Title = "PÁGINA NO ENCONTRADA";
                    ViewBag.Description = "Esta página no está disponible, no existe o no se puede encontrar.";
                    break;
            }

            return View();
        }

        public ActionResult Error404()
        {
            ViewBag.code = "404";
            ViewBag.Title = "PÁGINA NO ENCONTRADA";
            ViewBag.Description = "Esta página no está disponible, no existe o no se puede encontrar.";
            return View("Error");
        }
        
    }
}