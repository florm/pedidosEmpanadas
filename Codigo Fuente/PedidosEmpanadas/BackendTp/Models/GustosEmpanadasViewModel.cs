using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class GustosEmpanadasViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool IsSelected { get; set; }

        public GustosEmpanadasViewModel() { }
        public GustosEmpanadasViewModel(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            IsSelected = false;
        }
    }
}