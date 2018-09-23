using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackendTp.Controllers
{
    public class ErrorHandlerController : BaseController
    {
        public ActionResult Error()
        {
            return View();
        }
    }
}