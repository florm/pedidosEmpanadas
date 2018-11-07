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
            return Sesion.IdUsuario != 0 ? (ActionResult) RedirectToAction("Lista","Pedido") : View();
        }

        public JsonResult Registro(Usuario usuario)
        {
            _servicioUsuario.CrearUsuario(usuario);
            return Json("");
        }

        public JsonResult LoginOk(Usuario usuario)
        {
            var usuarioLogueado = _servicioUsuario.Login(usuario);
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

        //        public ActionResult Error()
        //        {
        //            ViewBag.MensajeDeError = RouteData.Values["Error"];
        //            return View();
        //        }

        
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

                //case 404:
                //    ViewBag.Title = "PÁGINA NO ENCONTRADA";
                //    ViewBag.Description = "Esta página no está disponible, no existe o no se puede encontrar.";
                //    break;
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