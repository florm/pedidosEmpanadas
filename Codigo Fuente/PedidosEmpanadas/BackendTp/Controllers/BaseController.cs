﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exceptions;

namespace BackendTp.Controllers
{
    [CustomExceptionFilter]
    public class BaseController : Controller
    {
        
        public BaseController()
        {
            
        }

        
    }
}