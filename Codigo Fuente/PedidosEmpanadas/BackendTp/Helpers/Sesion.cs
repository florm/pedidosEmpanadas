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

        public static int IdUsuario
        {
            get => Convert.ToInt32(HttpContext.Current.Session["IdUsuario"]);
            set => HttpContext.Current.Session["IdUsuario"] = value;
        }

        public static string EmailUsuario
        {
            get
            {
                if (HttpContext.Current.Session["EmailUsuario"] != null)
                {
                    return HttpContext.Current.Session["EmailUsuario"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set => HttpContext.Current.Session["EmailUsuario"] = value;
        }
    }
}