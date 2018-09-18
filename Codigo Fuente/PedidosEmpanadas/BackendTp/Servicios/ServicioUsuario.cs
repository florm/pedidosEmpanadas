using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

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

        private void ValidarEmail()
        {

        }
    }
}