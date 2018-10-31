﻿using BackendTp.Models;
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

        public Pedido GetById(int id)
        {
            return Db.Pedido.Include("GustoEmpanada").FirstOrDefault(p => p.IdPedido == id);
        }

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
            gustosSeleccionados.ForEach(gusto => pedido.GustoEmpanada.Add(gusto));
            Db.Pedido.Add(pedido);
            Db.SaveChanges();
            return pedido;
        }

        public List<Pedido> Lista(int IdUsuario)
        {
            List<Pedido> pedidosQuery = new List<Pedido>();

            pedidosQuery = (from Pedido p in Db.Pedido.Include("EstadoPedido")
                            join InvitacionPedido i in Db.InvitacionPedido
                            on p.IdPedido equals i.IdPedido
                            where i.IdUsuario == IdUsuario
                            select p).OrderByDescending(p => p.FechaCreacion).ToList();

            if (pedidosQuery == null)
            {
                return null;
            }

            return pedidosQuery;
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

    }
}