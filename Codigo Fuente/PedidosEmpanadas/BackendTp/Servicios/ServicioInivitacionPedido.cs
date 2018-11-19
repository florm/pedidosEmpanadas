using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using BackendTp.Helpers;
using Exceptions;
using GustoEmpanadasCantidad = BackendTp.Models.GustoEmpanadasCantidad;
using PedidoRequest = BackendTp.Models.PedidoRequest;

namespace BackendTp.Servicios
{
    public class ServicioInvitacionPedido: Servicio
    {
        private readonly ServicioUsuario _servicioUsuario;
        private readonly ServicioEmail _servicioMail;

        public ServicioInvitacionPedido(Entities context) : base(context)
        {
            _servicioUsuario = new ServicioUsuario(context);
            _servicioMail = new ServicioEmail(context);
        }

        public List<InvitacionPedido> Crear(List<UsuarioViewModel> invitados)
        {
            var idUsuarios = GetInvitados(invitados,  Sesion.IdUsuario);
            List<InvitacionPedido> invitaciones = new List<InvitacionPedido>();
            
            foreach(var id in idUsuarios)
            {
                InvitacionPedido invitacion = new InvitacionPedido
                {
                    Usuario = _servicioUsuario.GetById(id),
                    Token = Guid.NewGuid(),
                    Completado = false
                };

                invitaciones.Add(invitacion);
            }

            return invitaciones;
        }

        public InvitacionPedidoGustoEmpanadaUsuario ElegirGustos()
        {
            return null;
        }

        private List<int> GetInvitados (List<UsuarioViewModel> invitados, [Optional, DefaultParameterValue(0)] int idUsuarioResponsable)
        {
            List<int> idUsuarios = new List<int>();
            foreach(var invitado in invitados)
            {
                var usuario = Db.Usuario.FirstOrDefault(u => 
                string.Equals(u.Email, invitado.Email));
                if(usuario != null)
                    idUsuarios.Add(usuario.IdUsuario);
            }
            //se agrega tambien como invitado al usuario que realizo inicio el pedido
            if(idUsuarioResponsable!=0)
                idUsuarios.Add((int)idUsuarioResponsable);
            return idUsuarios;
        }

        public List<UsuarioViewModel> GetByIdPedido(Pedido pedido, int usuarioSesion)
        {
            return Db.InvitacionPedido.Where(ip => ip.IdPedido == pedido.IdPedido && ip.IdUsuario != usuarioSesion)
                .Select(ip=>new UsuarioViewModel{Id = ip.Usuario.IdUsuario, Email = ip.Usuario.Email, CompletoPedido = ip.Completado}).ToList();
        }

        public InvitacionPedido GetInvitacionPedidoPorPedido(int id, int idUsuario)
        {
            return Db.InvitacionPedido.FirstOrDefault(ip => ip.IdPedido == id && ip.IdUsuario == idUsuario);
        }

        public void Modificar(Pedido pedido,PedidoGustosEmpanadasViewModel pge)
        {
            var invitadosModel = GetInvitados(pge.Invitados, Sesion.IdUsuario);
            
            foreach (var invitacionPedido in pedido.InvitacionPedido.Reverse<InvitacionPedido>())
            {
                if (!invitadosModel.Contains(invitacionPedido.IdUsuario))//Borro las invicationes que no se encuentran en las nuevas
                {
                    //pedido.InvitacionPedido.Remove(invitacionPedido);//porque no funciona de esta manera????? Error de FK no nulleable.
                    Db.InvitacionPedido.Remove(invitacionPedido);
                }
                else //como ya se encuentran las borro del viewmodel para no volverlos a invitar
                    pge.Invitados.RemoveAll(x => x.Email == invitacionPedido.Usuario.Email);
            }
            
            List<InvitacionPedido> nuevosInvitados = GetNuevosInvitados(pge.Invitados);
           
            if (nuevosInvitados.Count != 0)
            {
                pedido.InvitacionPedido.AddRange(nuevosInvitados);
            }
               
           EnviarMailInicioPedido(nuevosInvitados, pedido, pge.Acciones);
            
        }

        public bool ConfirmarGustos(PedidoRequest pedido)
        {
            try
            {
                var listaDeGustosPorUsuario = Db.InvitacionPedidoGustoEmpanadaUsuario.Where(ip => ip.IdPedido == pedido.IdPedido && ip.IdUsuario == pedido.IdUsuario).ToList();

                foreach (InvitacionPedidoGustoEmpanadaUsuario inv in listaDeGustosPorUsuario)
                {
                    Db.InvitacionPedidoGustoEmpanadaUsuario.Remove(inv);
                }

                foreach (GustoEmpanadasCantidad g in pedido.GustoEmpanadasCantidad)
                {

                    if (g.Cantidad > 0)
                    {
                        Db.InvitacionPedidoGustoEmpanadaUsuario.Add(new InvitacionPedidoGustoEmpanadaUsuario
                        {
                            Cantidad = g.Cantidad,
                            IdGustoEmpanada = g.IdGustoEmpanada,
                            IdPedido = pedido.IdPedido,
                            IdUsuario = pedido.IdUsuario,
                        });
                    }

                }

                var invPedido = Db.InvitacionPedido.FirstOrDefault(i => i.IdPedido == pedido.IdPedido && i.IdUsuario == pedido.IdUsuario);
                invPedido.Completado = true;

                Db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void EnviarMailInicioPedido(List<InvitacionPedido> nuevosInvitados, Pedido pedido, int accion)
        {
            switch (accion)
            {
                case (int)EmailAcciones.ANadie:
                    break;
                case (int)EmailAcciones.EnviarSoloALosNuevos:
                    if (nuevosInvitados.Count != 0)
                    {
                        _servicioMail.EnviarANuevos(nuevosInvitados);

                    }
                    break;
                case (int)EmailAcciones.ReEnviarInvitacionATodos:
                    _servicioMail.EnviarMailInicioPedido(pedido);
                    break;
                case (int)EmailAcciones.ReEnviarSoloALosQueNoEligieronGustos:
                    _servicioMail.EnviarALosQueNoEligieronGusto(pedido);
                    break;
            }
        }
        
        public List<InvitacionPedido> GetNuevosInvitados(List<UsuarioViewModel> invitados)
        {
            var idUsuarios = GetInvitados(invitados);
            List<InvitacionPedido> invitaciones = new List<InvitacionPedido>();
            
            foreach(var id in idUsuarios)
            {
                InvitacionPedido invitacion = new InvitacionPedido
                {
                    Usuario = _servicioUsuario.GetById(id),
                    Token = Guid.NewGuid(),
                    Completado = false
                };

                invitaciones.Add(invitacion);
            }

            return invitaciones;
        }
    }
}