using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using BackendTp.Helpers;
using Exceptions;
using GustoEmpanadasCantidad = BackendTp.Models.GustoEmpanadasCantidad;
using PedidoRequest = BackendTp.Models.PedidoRequest;

namespace BackendTp.Servicios
{
    public class ServicioInvitacionPedido: Servicio
    {
        //private static readonly Entities Context = new Entities();
        private readonly ServicioUsuario _servicioUsuario;

        public ServicioInvitacionPedido(Entities context) : base(context)
        {
            _servicioUsuario = new ServicioUsuario(context);
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

        private List<int> GetInvitados (List<UsuarioViewModel> invitados, int? idUsuarioResponsable)
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
            if(idUsuarioResponsable!=null)
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

        public void Modificar(int idPedido, List<UsuarioViewModel> invitados, int accion)
        {
            var invitacionPedidoModel = Db.InvitacionPedido.Where(ip => ip.IdPedido == idPedido).ToList();
            var invitadosModel = GetInvitados(invitados, Sesion.IdUsuario);
            foreach (var invitacionPedido in invitacionPedidoModel)
            {
                if (!invitadosModel.Contains(invitacionPedido.IdUsuario))
                    Db.InvitacionPedido.Remove(invitacionPedido);
            }
            var nuevosInvitados = new List<int>();
            foreach (var invitado in invitadosModel)
            {
                if (!invitacionPedidoModel.Select(ip => ip.IdUsuario).Contains(invitado))
                {
                    var nuevaInvitacionPedido = new InvitacionPedido
                    {
                        IdUsuario = invitado,
                        IdPedido = idPedido,
                        Token = Guid.NewGuid(),
                        Completado = false
                    };
                    Db.InvitacionPedido.Add(nuevaInvitacionPedido);
                    nuevosInvitados.Add(nuevaInvitacionPedido.IdUsuario);
                }
            }

            Db.SaveChanges();
            //EnviarMailInicioPedido(nuevosInvitados, idPedido, accion);
            
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

                Db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

//        public void EnviarMailInicioPedido(List<int> nuevosInvitados, int idPedido, int accion)
//        {
//            ServicioEmail servicioMail = new ServicioEmail();
//            switch (accion)
//            {
//                case (int)EmailAcciones.ANadie:
//                    break;
//                case (int)EmailAcciones.EnviarSoloALosNuevos:
//                    servicioMail.ArmarMailInicioPedido(nuevosInvitados, idPedido);
//                    break;
//                case (int)EmailAcciones.ReEnviarInvitacionATodos:
//                    var todosLosInivitados = Db.InvitacionPedido.Where(ip => ip.IdPedido == idPedido)
//                        .Select(i => i.IdUsuario)
//                        .ToList();
//                    servicioMail.ArmarMailInicioPedido(todosLosInivitados, idPedido);
//                    break;
//                case (int)EmailAcciones.ReEnviarSoloALosQueNoEligieronGustos:
//                    var invitadosSinGustos = Db.InvitacionPedido.Where(ip => ip.IdPedido == idPedido
//                                                                             && ip.Completado == false)
//                        .Select(i => i.IdUsuario)
//                        .ToList();
//                    servicioMail.ArmarMailInicioPedido(invitadosSinGustos, idPedido);
//                    break;
//            }
//        }
    }
}