using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BackendTp.Helpers;
using BackendTp.Models;
using BackendTp.Servicios;
using Newtonsoft.Json;

namespace BackendTp.Controllers
{
    public class MailController : BaseController
    {
        private readonly  ServicioEmail _servicioEmail = new ServicioEmail();

        
        public JsonResult Confirmar([FromBody] EmailViewModel emailViewModel)
        {
            _servicioEmail.ArmarMailsConfirmacion(emailViewModel);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}