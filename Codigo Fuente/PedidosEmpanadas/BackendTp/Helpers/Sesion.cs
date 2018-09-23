using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using BackendTp.Models;

namespace BackendTp.Helpers
{
    public class Sesion
    {

        public static Usuario Usuario
        {
            get => HttpContext.Current.Session["Usuario"] as Usuario;
            set => HttpContext.Current.Session["Usuario"] = value;
        }
    }
}