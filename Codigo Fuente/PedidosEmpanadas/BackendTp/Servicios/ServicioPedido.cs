using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;
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
        private readonly ServicioGustoEmpanada _servicioGustoEmpanada;
        private readonly ServicioInvitacionPedido _servicioInvitacionPedido;
        private readonly ServicioEmail _servicioEmail;
        private readonly ServicioUsuario _servicioUsuario;
        private readonly ServicioEstadoPedido _servicioEstadoPedido;
        
        public ServicioPedido(Entities context) : base(context)
        {
            _servicioGustoEmpanada = new ServicioGustoEmpanada(context);
            _servicioInvitacionPedido = new ServicioInvitacionPedido(context);
            _servicioEmail = new ServicioEmail(context);
            _servicioUsuario = new ServicioUsuario(context);
            _servicioEstadoPedido = new ServicioEstadoPedido(context);
        }
        
        public PedidoGustosEmpanadasViewModel  Iniciar()
        {
            PedidoGustosEmpanadasViewModel pgeVm = new PedidoGustosEmpanadasViewModel();
            var gustos = _servicioGustoEmpanada.GetAll();
            foreach (var gusto in gustos)
            {
                pgeVm.GustosDisponibles.Add(new GustosEmpanadasViewModel(gusto.IdGustoEmpanada, gusto.Nombre));
            }
            return pgeVm;
        }
        
        public List<Pedido> GetAll()
        {
            return Db.Pedido.ToList();
        }

        public Pedido GetById(int id, [Optional, DefaultParameterValue(null)] string[] includes)
        {
            var pedidoQuery = Db.Pedido;
            if (includes != null)
            {
                foreach (var relacion in includes)
                {
                    pedidoQuery.Include(relacion);
                }  
            }
            
            Pedido pedido = pedidoQuery.FirstOrDefault(p => p.IdPedido == id);
            if (pedido == null)
                throw new IdNoEncontradoException(Genero.Masculino, "Pedido");
            return pedido;
        }

        public InvitacionPedido GetInvitacion(int idPedido, int idUsuario)
        {
            return Db.InvitacionPedido.FirstOrDefault(p => p.IdPedido == idPedido && p.IdUsuario == idUsuario);
        }

        public Pedido Crear(PedidoGustosEmpanadasViewModel pge)
        {
            var pedido = pge.Pedido;
            pedido.FechaCreacion = DateTime.Now;
            pedido.Usuario = _servicioUsuario.GetById(Sesion.IdUsuario);
            pedido.EstadoPedido = _servicioEstadoPedido.GetAbierto();
            pedido.GustoEmpanada = _servicioGustoEmpanada.ObtenerGustos(pge.GustosDisponibles);

            List<InvitacionPedido> invitaciones = _servicioInvitacionPedido.Crear(pge.Invitados);
            pedido.InvitacionPedido.AddRange(invitaciones);
                        
            Db.Pedido.Add(pedido);
            Db.SaveChanges();
            _servicioEmail.EnviarMailInicioPedido(pedido);

            return pedido;
        }

        public List<Pedido> Lista(int idUsuario)
        {            
            var pedidosQuery = (from Pedido p in Db.Pedido.Include("EstadoPedido")
                join InvitacionPedido i in Db.InvitacionPedido
                    on p.IdPedido equals i.IdPedido
                where (i.IdUsuario == idUsuario || p.IdUsuarioResponsable == idUsuario)
                
                select p).Distinct().OrderByDescending(p => p.FechaCreacion).ToList();

            return pedidosQuery;
        }

        public int Modificar(PedidoGustosEmpanadasViewModel pge)
        {
            if(pge.Pedido == null)
                throw new IdNoEncontradoException(Genero.Masculino, "Pedido");
            
            var pedido = GetById(pge.Pedido.IdPedido, new []{"InvitacionPedido"});
            pedido.NombreNegocio = pge.Pedido.NombreNegocio;
            pedido.Descripcion = pge.Pedido.Descripcion;
            pedido.PrecioUnidad = pge.Pedido.PrecioUnidad;
            pedido.PrecioDocena = pge.Pedido.PrecioDocena;
            pedido.FechaModificacion = DateTime.Now;
            pedido.GustoEmpanada = _servicioGustoEmpanada.ObtenerGustos(pge.GustosDisponibles);
            
            _servicioInvitacionPedido.Modificar(pedido, pge);

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
            if (!pedidoDetalle.InvitacionPedido.Any(i=>i.IdUsuario == Sesion.IdUsuario))
                throw new PermisosException();

            return pedidoDetalle;
        }

        public void Confirmar(Pedido pedido)
        {
            var pedidoModel = GetById(pedido.IdPedido);
            pedidoModel.IdEstadoPedido = (int)EstadosPedido.Cerrado;
            Db.SaveChanges();

        }

        public PedidoGustosEmpanadasViewModel Clonar(int id)
        {
            Pedido pedido = GetById(id);
            var gustosModel = _servicioGustoEmpanada.GetAll();
            var invitados = _servicioInvitacionPedido.GetByIdPedido(pedido, Sesion.IdUsuario);
            var pgeVM = new PedidoGustosEmpanadasViewModel(pedido, pedido.GustoEmpanada.ToList(),gustosModel, invitados);
            
            return pgeVM;
        }

        public List<Usuario> getInvitados(Pedido pedido)
        {
            List<Usuario> invitados = new List<Usuario>();
            foreach (var invitacion in pedido.InvitacionPedido)
            {
                invitados.Add(invitacion.Usuario);
            }

            return invitados;
        }

        public ElegirGustosVm ElegirGustosPorToken(Guid token)
        {
            var invitacion = Db.InvitacionPedido.FirstOrDefault(i => i.Token == token);
            if(invitacion?.IdUsuario != Sesion.IdUsuario)
                  throw  new PermisosException();
            var gpu = ElegirGustosUsuario(invitacion.IdPedido, invitacion.IdUsuario);
            return gpu;
        }

        public ElegirGustosVm ElegirGustosUsuario(int id, int usuarioId)
        {
            var pedido = GetById(id);
            var gustosEnPedido = pedido.GustoEmpanada.ToList();
            var gustosElegidosUsuario = _servicioGustoEmpanada.GetGustosDeUsuario(id, usuarioId);
            var invitacionPedido = GetInvitacion(id, usuarioId);
            ElegirGustosVm gustosDeUsuario = new ElegirGustosVm();
            List<GustoEmpanadasCantidad> empanadas = new List<GustoEmpanadasCantidad>();

            foreach (GustoEmpanada gu in gustosEnPedido)
            {
                empanadas.Add(new GustoEmpanadasCantidad() { IdGustoEmpanada = gu.IdGustoEmpanada, Nombre = gu.Nombre, Cantidad = 0 });
            }

            foreach (InvitacionPedidoGustoEmpanadaUsuario g in gustosElegidosUsuario)
            {
                foreach (GustoEmpanadasCantidad gu in empanadas)
                {
                    if (g.IdGustoEmpanada == gu.IdGustoEmpanada)
                    {
                        gu.Cantidad = g.Cantidad;
                    }
                }
            }

            gustosDeUsuario.Token = invitacionPedido.Token;
            gustosDeUsuario.Pedido = pedido;
            gustosDeUsuario.Empanadas = empanadas;

            return gustosDeUsuario;
        }

        public List<PedidoViewModel> ListarPedidosMobile(int idUsuario)
        {
            List<PedidoViewModel> ListaPedido = new List<PedidoViewModel>();
            var pedidos = Lista(idUsuario);
            foreach (Pedido p in pedidos)
            {
                ListaPedido.Add(new PedidoViewModel
                {
                    IdPedido = p.IdPedido,
                    IdUsuarioResponsable = p.IdUsuarioResponsable,
                    FechaCreacion = p.FechaCreacion,
                    NombreNegocio = p.NombreNegocio,
                    Estado = p.IdEstadoPedido,
                    Rol = p.IdUsuarioResponsable,
                    EstadoS = p.EstadoPedido.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioDocena = p.PrecioDocena,
                    PrecioUnidad = p.PrecioUnidad
                });
            }

            foreach (PedidoViewModel pvm in ListaPedido)
            {
                if (pvm.Rol == idUsuario)
                {
                    pvm.RolS = "Responsable";
                }
                else
                {
                    pvm.RolS = "Invitado";
                }

            }

            return ListaPedido;
        }

        public List<GustoEmpanadasCantidad> GetGustosForMobile(int id, int usuarioId)
        {
            ElegirGustosVm Gustos = ElegirGustosUsuario(id, usuarioId);
            List<GustoEmpanadasCantidad> Empanadas = new List<GustoEmpanadasCantidad>();
            foreach (GustoEmpanadasCantidad g in Gustos.Empanadas)
            {
                Empanadas.Add(new GustoEmpanadasCantidad() { Nombre = g.Nombre, Cantidad = g.Cantidad, IdGustoEmpanada = g.IdGustoEmpanada });
            }
            return Empanadas;
        }
    }
}