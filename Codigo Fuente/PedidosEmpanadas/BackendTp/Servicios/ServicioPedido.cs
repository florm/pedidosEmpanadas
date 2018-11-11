using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using BackendTp.Helpers;
using Exceptions;
using BackendTp.Enums;
using Exceptions.Enums;
using Microsoft.Ajax.Utilities;

namespace BackendTp.Servicios
{
    public class ServicioPedido : Servicio
    {
        private readonly ServicioGustoEmpanada _servicioGustoEmpanada = new ServicioGustoEmpanada();
        private readonly ServicioInvitacionPedido _servicioInvitacionPedido = new ServicioInvitacionPedido();
        
        public List<Pedido> GetAll()
        {
            return Db.Pedido.ToList();
        }

        public Pedido GetById(int id)
        {
            var pedido = Db.Pedido.Include("GustoEmpanada").FirstOrDefault(p => p.IdPedido == id);
            if (pedido == null)
                throw new IdNoEncontradoException(Genero.Masculino, "Pedido");
            return pedido;
        }

        public InvitacionPedido GetInvitacion(int idPedido, int idUsuario)
        {
            return Db.InvitacionPedido.FirstOrDefault(p => p.IdPedido == idPedido && p.IdUsuario == idUsuario);
        }

        //public string GetToken(int idPedido, int idUsuario)
        //{
        //    return Db.InvitacionPedido.Where(i => i.IdPedido == idPedido).Where(i => i.IdUsuario == idUsuario).Select(i => i.Token).ToString();
        //}

        public Pedido Crear(PedidoGustosEmpanadasViewModel pge)
        {
            var pedido = pge.Pedido;
            pedido.FechaCreacion = DateTime.Now;
            pedido.IdUsuarioResponsable = Sesion.IdUsuario;
            pedido.IdEstadoPedido = (int)EstadosPedido.Abierto;
            List<GustoEmpanada> gustosSeleccionados = new List<GustoEmpanada>();
            foreach (var gusto in pge.GustosDisponibles)
            {
                if (gusto.IsSelected)
                    gustosSeleccionados.Add(Db.GustoEmpanada.FirstOrDefault(ge => ge.IdGustoEmpanada == gusto.Id));
            }
            //gustosSeleccionados.ForEach(gusto => pedido.GustoEmpanada.Add(gusto));
            pedido.GustoEmpanada = gustosSeleccionados;
            Db.Pedido.Add(pedido);
            Db.SaveChanges();
            return pedido;
        }

        public List<Pedido> Lista(int idUsuario)
        {
            //List<Pedido> pedidosQuery = new List<Pedido>();

//            pedidosQuery = (from Pedido p in Db.Pedido.Include("EstadoPedido")
//                            join InvitacionPedido i in Db.InvitacionPedido
//                            on p.IdPedido equals i.IdPedido
//                            where i.IdUsuario == IdUsuario
//                            select p).OrderByDescending(p => p.FechaCreacion).ToList();
            
            var pedidosQuery = (from Pedido p in Db.Pedido.Include("EstadoPedido")
                join InvitacionPedido i in Db.InvitacionPedido
                    on p.IdPedido equals i.IdPedido
                where (i.IdUsuario == idUsuario || p.IdUsuarioResponsable == idUsuario)
                
                select p).OrderByDescending(p => p.FechaCreacion).Distinct().ToList();

            return pedidosQuery;
        }

        public int Modificar(PedidoGustosEmpanadasViewModel pge)
        {
            if(pge.Pedido == null)
                throw new IdNoEncontradoException(Genero.Masculino, "Pedido");
            var pedido = GetById(pge.Pedido.IdPedido);
            pedido.NombreNegocio = pge.Pedido.NombreNegocio;
            pedido.Descripcion = pge.Pedido.Descripcion;
            pedido.PrecioUnidad = pge.Pedido.PrecioUnidad;
            pedido.PrecioDocena = pge.Pedido.PrecioDocena;
            pedido.FechaModificacion = DateTime.Now;
            pedido.IdEstadoPedido = (int)EstadosPedido.Cerrado;
            List<GustoEmpanada> gustosSeleccionados = new List<GustoEmpanada>();
            foreach (var gusto in pge.GustosDisponibles)
            {
                if (gusto.IsSelected)
                    gustosSeleccionados.Add(Db.GustoEmpanada.FirstOrDefault(ge => ge.IdGustoEmpanada == gusto.Id));
            }

            pedido.GustoEmpanada = gustosSeleccionados;
            Db.SaveChanges();
            return pedido.IdPedido;
        }

        public void Eliminar(int id)
        {
            Pedido pedido = Db.Pedido
                .Include("InvitacionPedidoGustoEmpanadaUsuario")
                .Include("GustoEmpanada")
                .Include("InvitacionPedido")
                .FirstOrDefault(p => p.IdPedido == id);
            
            Db.InvitacionPedidoGustoEmpanadaUsuario.RemoveRange(pedido.InvitacionPedidoGustoEmpanadaUsuario);
            Db.InvitacionPedido.RemoveRange(pedido.InvitacionPedido);
         
            //otra manera de borrar los relacionados
            //pedido.InvitacionPedidoGustoEmpanadaUsuario.ToList().ForEach(s => Db.Entry(s).State = EntityState.Deleted);
            //pedido.InvitacionPedido.ToList().ForEach(s => Db.Entry(s).State = EntityState.Deleted);
            
            Db.Pedido.Remove(pedido);
            Db.SaveChanges();
        }
        
        public Pedido Detalle(Pedido pedido)
        {
            Pedido pedidoDetalle = new Pedido();

            pedidoDetalle = (from Pedido p in Db.Pedido.Include("EstadoPedido").Include("GustoEmpanada").Include("InvitacionPedido").Include("Usuario")
                            where p.IdPedido == pedido.IdPedido
                            select p).FirstOrDefault();

            if (pedidoDetalle == null)
            {
                return null;
            }

            return pedidoDetalle;
        }

        public PedidoGustosEmpanadasViewModel Clonar(int id)
        {
            
            Pedido pedido = GetById(id);
            var gustosModel = _servicioGustoEmpanada.GetAll();
            var invitados = _servicioInvitacionPedido.GetByIdPedido(pedido, Sesion.IdUsuario);
            var pgeVM = new PedidoGustosEmpanadasViewModel(pedido, pedido.GustoEmpanada.ToList(),gustosModel, invitados);
            
            return pgeVM;
        }
    }
}