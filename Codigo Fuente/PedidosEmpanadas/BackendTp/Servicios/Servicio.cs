using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackendTp.Servicios
{
    public class Servicio
    {
        protected Entities Db { get; }

        public Servicio()
        {
            Db = new Entities();
        }
    }
}