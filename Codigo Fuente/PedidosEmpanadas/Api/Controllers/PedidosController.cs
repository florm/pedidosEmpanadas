﻿using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace Api.Controllers
{
    //[Route("api/[controller]")]
    //[EnableCors(origins: "http://localhost:57162/", headers: "*", methods: "*")]
    public class PedidosController : ApiController
    {
        [HttpPost]
        public PedidoRequest ConfirmarGustos([FromBody] PedidoRequest pedido)
        {

            return null;
        }

    }
}
