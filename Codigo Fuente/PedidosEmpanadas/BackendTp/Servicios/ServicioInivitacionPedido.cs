using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Exceptions;

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
    }
}