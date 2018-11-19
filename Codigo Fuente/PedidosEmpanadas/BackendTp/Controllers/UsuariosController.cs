using BackendTp.Models;
using BackendTp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BackendTp.Controllers
{
    public class UsuariosController : ApiController
    {
        private static readonly Entities Context = new Entities();

        private readonly ServicioUsuario servicioUsuario = new ServicioUsuario(Context);

        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetMobileUser(string id, string param2)
        {
            var usuario = servicioUsuario.LoginMobile(id, param2);
            return Ok(usuario);
        }

        [ResponseType(typeof(string))]
        public IHttpActionResult PostNewUser(Usuario usuario)
        {
            var valida = servicioUsuario.CrearUsuario(usuario);
            string respuesta = valida.Item1;
            return Ok(respuesta);
        }

    }
}
