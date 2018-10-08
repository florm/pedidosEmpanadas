using BackendTp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Exceptions;

namespace BackendTp.Servicios
{
    public class ServicioGustoEmpanada: Servicio
    {

        public List<GustoEmpanada> GetAll()
        {
            return Db.GustoEmpanada.ToList();
        }

        
    }
}