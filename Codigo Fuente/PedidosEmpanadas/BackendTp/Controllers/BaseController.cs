using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackendTp.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ValidarUsuarioDeslogueado();
        }

        private void ValidarUsuarioDeslogueado()
        {

        }
    }
}