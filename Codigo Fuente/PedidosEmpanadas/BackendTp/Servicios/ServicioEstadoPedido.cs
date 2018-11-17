using BackendTp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using BackendTp.Models;
using WebGrease.Css.Extensions;

namespace BackendTp.Servicios
{
    public class ServicioEstadoPedido : Servicio
    {
        public EstadoPedido GetAbierto()
        {
            return Db.EstadoPedido.FirstOrDefault(p => p.Nombre == "Abierto");
        }
        
        public EstadoPedido GetCerrado()
        {
            return Db.EstadoPedido.FirstOrDefault(p => p.Nombre == "Cerado");
        }
    }
}