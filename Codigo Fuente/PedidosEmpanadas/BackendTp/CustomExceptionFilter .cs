﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BackendTp.Helpers;
using Exceptions;

namespace BackendTp
{
    public class CustomExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private ActionExecutingContext _context;


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _context = context;

            base.OnActionExecuting(context);
            ValidarUsuarioLogueado();
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is UsuarioDeslogueadoException excUsuario)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectResult(excUsuario.PathRedirect);
            }

            else if (filterContext.Exception is PedidosEmpanadasException pe)
            {
                ArmarMsgRespuestaError(filterContext, filterContext.Exception.Message);
            }
            else if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = AjaxException(filterContext.Exception.Message, filterContext);
            }
            else
            {
                ArmarMsgRespuestaErrorGenerica(filterContext);
            }
        }

        public void ValidarUsuarioLogueado()
        {
            Sesion.UltimaUrlAccedida = _context.HttpContext.Request.FilePath + ((_context.HttpContext.Request.QueryString.Count != 0) ? "?"+ _context.HttpContext.Request.QueryString : "");
            if(Sesion.IdUsuario == 0)
                throw new UsuarioDeslogueadoException();
        }

        private JsonResult AjaxException(string message, ExceptionContext filterContext)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "Server error";
            }

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;       //Needed for IIS7.0

            return new JsonResult
            {
                Data = new { ErrorMessage = message },
                ContentEncoding = Encoding.UTF8,
            };
        }

        public static void ArmarMsgRespuestaError(ExceptionContext context, string msgError)
        {
            context.ExceptionHandled = true;
            context.RouteData.Values.Add("Error", msgError);
            context.Result = new RedirectResult("~/Home/Error?error=405&mensaje="+msgError);
            var response = context.HttpContext.Response;
            response.ContentType = "text/plain";
            response.StatusCode = 400;
            response.Write(msgError);
        }

        public static void ArmarMsgRespuestaErrorGenerica(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.Result = new RedirectResult("~/Home/Error?error=505");
        }
    }
}