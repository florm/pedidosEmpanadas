using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Exceptions;

namespace BackendTp.Servicios
{
    public class ServicioGustoEmpanada: Servicio
    {
        public ServicioGustoEmpanada(Entities context) : base(context)
        {   
        }
        
        public List<GustoEmpanada> ObtenerGustos(List<GustosEmpanadasViewModel> pgeGustosDisponibles)
        {
            List<GustoEmpanada> gustosSeleccionados = new List<GustoEmpanada>();
            foreach (var gusto in pgeGustosDisponibles)
            {
                if (gusto.IsSelected)
                    gustosSeleccionados.Add(GetById(gusto.Id));
            }

            return gustosSeleccionados;
        }

        public GustoEmpanada GetById(int id)
        {
            return Db.GustoEmpanada.Single(e => e.IdGustoEmpanada == id);
        }

        public List<GustoEmpanada> GetAll()
        {
            return Db.GustoEmpanada.ToList();
        }

        public List<GustoEmpanada> GetGustosEnPedido(int idPedido)
        {
            //return Db.GustoEmpanada.Where(ip => ip == idPedido).ToList();
            var pedido = Db.Pedido.Include("GustoEmpanada").FirstOrDefault(p => p.IdPedido == idPedido);
            return pedido.GustoEmpanada.ToList();
        }

        public List<InvitacionPedidoGustoEmpanadaUsuario> GetGustosDeUsuario(int idPedido, int idUsuario)
        {
            //var pedido = Db.InvitacionPedidoGustoEmpanadaUsuario.Include("GustoEmpanada").FirstOrDefault(p => p.IdPedido == idPedido && p.IdUsuario == idUsuario);
            //return pedido.InvitacionPedidoGustoEmpanadaUsuario.ToList();
            return Db.InvitacionPedidoGustoEmpanadaUsuario.Where(ip => ip.IdPedido == idPedido && ip.IdUsuario == idUsuario).ToList();
        }
    }
}