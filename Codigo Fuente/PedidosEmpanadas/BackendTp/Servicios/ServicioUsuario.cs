﻿using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Exceptions;

namespace BackendTp.Servicios
{
    public class ServicioUsuario : Servicio
    {
        
        public void CrearUsuario(Usuario usuario)
        {
            ValidarEmail();
            Db.Usuario.Add(usuario);
            Db.SaveChanges();
        }

        public Usuario Login(Usuario usuario)
        {
            var user = Db.Usuario.FirstOrDefault(u => u.Email == usuario.Email && u.Password == usuario.Password);
            if(user == null)
                throw new UsuarioDeslogueadoException();
            return user;
        }

        private void ValidarEmail()
        {

        }
    }
}