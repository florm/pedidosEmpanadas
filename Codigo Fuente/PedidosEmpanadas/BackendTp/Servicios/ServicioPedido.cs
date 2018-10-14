using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using BackendTp.Helpers;
using Exceptions;
using BackendTp.Enums;

namespace BackendTp.Servicios
{
    public class ServicioPedido : Servicio
    {

        public List<Pedido> GetAll()
        {
            return Db.Pedido.ToList();
        }

        public Pedido Crear(PedidoGustosEmpanadasViewModel pge)
        {
            var pedido = pge.Pedido;
            pedido.FechaCreacion = DateTime.Now;
            pedido.IdUsuarioResponsable = Sesion.IdUsuario;
            pedido.IdEstadoPedido = (int)EstadosPedido.Abierto;
            List<GustoEmpanada> gustos = new List<GustoEmpanada>();
            foreach (var gusto in pge.GustosDisponibles)
            {
                if (gusto.IsSelected)
                    gustos.Add(Db.GustoEmpanada.FirstOrDefault(ge => ge.IdGustoEmpanada == gusto.Id));
            }
            gustos.ForEach(gusto => pedido.GustoEmpanada.Add(gusto));
            Db.Pedido.Add(pedido);
            Db.SaveChanges();
            return pedido;
        }
        
    }
}