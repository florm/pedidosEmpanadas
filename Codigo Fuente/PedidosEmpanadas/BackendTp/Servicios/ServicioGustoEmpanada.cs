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

        public int CantidadTotalDeEmpanadas(int idPedido)
        {
            if (idPedido != 0)
            {
                var ipgeu = Db.InvitacionPedidoGustoEmpanadaUsuario.Where(i => i.IdPedido == idPedido).ToList();
                if (ipgeu.Count != 0)
                    return ipgeu.Sum(i => i.Cantidad);
            }
            return 0;
        }
    }
}