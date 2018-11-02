using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using BackendTp.Helpers;
using Exceptions;
using Api.Models;

namespace BackendTp.Servicios
{
    public class ServicioInvitacionPedido: Servicio
    {
        
        public void Crear(Pedido pedido, List<UsuarioViewModel> invitados, int idUsuarioResponsable)
        {
            var idUsuarios = GetInvitados(invitados, idUsuarioResponsable);
            foreach(var id in idUsuarios)
            {
                Db.InvitacionPedido.Add(new InvitacionPedido
                    {
                        IdPedido = pedido.IdPedido,
                        IdUsuario = id,
                        Token = Guid.NewGuid(),
                        Completado = false
                    });
                
            } 
            
            Db.SaveChanges();
        }

        public InvitacionPedidoGustoEmpanadaUsuario ElegirGustos()
        {
            return null;
        }

        private List<int> GetInvitados (List<UsuarioViewModel> invitados, int idUsuarioResponsable)
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
            idUsuarios.Add(idUsuarioResponsable);
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

        public void Modificar(int idPedido, List<UsuarioViewModel> invitados)
        {
            var invitacionPedidoModel = Db.InvitacionPedido.Where(ip => ip.IdPedido == idPedido).ToList();
            var invitadosModel = GetInvitados(invitados, Sesion.IdUsuario);
            foreach (var invitacionPedido in invitacionPedidoModel)
            {
                if (!invitadosModel.Contains(invitacionPedido.IdUsuario))
                    Db.InvitacionPedido.Remove(invitacionPedido);
            }

            foreach (var invitado in invitadosModel)
            {
                if (!invitacionPedidoModel.Select(ip => ip.IdUsuario).Contains(invitado))
                {
                    var nuevaInvitacionPedido = new InvitacionPedido
                    {
                        IdUsuario = invitado,
                        IdPedido = idPedido
                    };
                    Db.InvitacionPedido.Add(nuevaInvitacionPedido);
                }
            }
            Db.SaveChanges();
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
    }
}