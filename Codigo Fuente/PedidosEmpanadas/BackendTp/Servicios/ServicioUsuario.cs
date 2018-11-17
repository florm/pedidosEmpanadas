using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Exceptions;
using UsuarioViewModel = BackendTp.Models.UsuarioViewModel;

namespace BackendTp.Servicios
{
    public class ServicioUsuario : Servicio
    {
        public Usuario GetById(int id)
        {
            return Db.Usuario.Single(u=> u.IdUsuario==id);
        } 
        
        public (string, string) CrearUsuario(Usuario usuario)
        {
            if(!UsuarioValido(usuario))
                return ("error","El usuario ya existe");
            Db.Usuario.Add(usuario);
            Db.SaveChanges();
            return ("ok", "El registro se realizó correctamente");
        }

        public Usuario Login(Usuario usuario)
        {
            var user = Db.Usuario.FirstOrDefault(u => u.Email == usuario.Email && u.Password == usuario.Password);
            if (user == null)
                throw new UsuarioInvalidoException();
            return user;
        }

        public List<Usuario> GetAll(string email)
        {
            return Db.Usuario.Where(u=> String.IsNullOrEmpty(email) || u.Email.Contains(email)).ToList();
        }

        public UsuarioViewModel GetByEmail(string email, bool completoPedido)
        {
            var usuario = Db.Usuario.FirstOrDefault(u => u.Email == email);
            if(usuario!=null)
            return new UsuarioViewModel(usuario.IdUsuario, usuario.Email, completoPedido);
            return null;
        }
        public List<UsuarioViewModel> GetAllByEmail(List<UsuarioViewModel> usuarioVm)
        {
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();
            foreach (var usuario in usuarioVm)
            {
                lista.Add(GetByEmail(usuario.Email, usuario.CompletoPedido));
            }

            return lista;
        }

        public void ValidarPermisoUsuario(int pedidoId, int usuarioId)
        {
            var pedido = Db.Pedido
                .FirstOrDefault(p => p.IdUsuarioResponsable == usuarioId && p.IdPedido == pedidoId);
            if(pedido==null)
                throw new PermisosException();

        }

        //App
        public Usuario LoginMobile(string email, string pass)
        {
            var user = Db.Usuario.FirstOrDefault(u => u.Email == email && u.Password == pass);
            if (user == null)
                return null;
            return user;
        }

        public bool UsuarioValido(Usuario usuario)
        {
            var usuarioModel = Db.Usuario.FirstOrDefault(u => u.Email == usuario.Email);
            if (usuarioModel != null)
                return false;
            return true;

        }
    }
}