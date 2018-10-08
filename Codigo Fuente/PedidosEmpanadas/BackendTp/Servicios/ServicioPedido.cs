using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using BackendTp.Helpers;
using Exceptions;

namespace BackendTp.Servicios
{
    public class ServicioPedido : Servicio
    {

        public List<Pedido> GetAll()
        {
            return Db.Pedido.ToList();
        }

        public Pedido Crear(Pedido pedido)
        {
            pedido.FechaCreacion = DateTime.Now;
            pedido.IdUsuarioResponsable = Sesion.IdUsuario;
            pedido.IdEstadoPedido = (int) EstadosPedido.Abierto;
            Db.Pedido.Add(pedido);
            Db.SaveChanges();
            return pedido;
        }
        
    }
}