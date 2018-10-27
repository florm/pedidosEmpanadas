using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class JsonResult
    {
        public List<GustoEmpanadasCantidad> GustoEmpanadasCantidad { get; set; }
        public int IdUsuario { get; set; }
        public int IdPedido { get; set; }
        public string Token { get; set; }
    }
}