using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace BackendTp
{
    public class ManejadorExceptionFilter : IExceptionFilter
    {
        
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
               filterContext.Result = AjaxException(filterContext.Exception.Message, filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
            }
        }

        private JsonResult AjaxException(string message, ExceptionContext filterContext)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "Ha ocurrido un error interno. Comuníquese con el Administrador"; 
            }

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;       //Needed for IIS7.0
            filterContext.ExceptionHandled = true;

            return new JsonResult
            {
                Data = new { ErrorMessage = message },
                ContentEncoding = Encoding.UTF8,
            };
        }
        
    }
}