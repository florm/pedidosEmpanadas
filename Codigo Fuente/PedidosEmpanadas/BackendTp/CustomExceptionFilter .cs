using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public void ValidarUsuarioLogueado()
        {
            if(Sesion.Usuario == null)
                throw new UsuarioDeslogueadoException();
        }
    }
}