using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BackendTp.Models;
using BackendTp.Servicios;

namespace BackendTp.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly ServicioUsuario _servicioUsuario = new ServicioUsuario();
        //ServicioUsuario ServicioUsuario = new ServicioUsuario();

        //public JsonResult Registro(Usuario usuario)
        //{
        //    usuario = null;
        //    ServicioUsuario.CrearUsuario(usuario);
        //    return Json("");
        //}


        //public ActionResult Login()
        //{
        //    return View();
        //}

        public JsonResult Consultar(UsuarioViewModel usuario)
        {
            var usuarios = _servicioUsuario.GetAll(usuario.Email);
            var usuariosViewModel = usuarios.Select(u => new UsuarioViewModel { Id = u.IdUsuario, Email = u.Email }).ToList();

            
            return Json(usuariosViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}