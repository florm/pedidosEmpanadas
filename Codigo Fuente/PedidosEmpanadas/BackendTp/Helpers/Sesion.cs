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

        public static int? IdUsuario
        {
            get
            {
                if (HttpContext.Current.Session["Usuario"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["Usuario"]);
                }
                else
                {
                    return null;
                }
            }
            set => HttpContext.Current.Session["Usuario"] = value;
        }
    }
}