using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public bool CompletoPedido { get; set; }

        public UsuarioViewModel() { }

        public UsuarioViewModel(int id, string email)
        {
            Id = id;
            Email = email;
        }

        public UsuarioViewModel(int id, string email, bool completoPedido)
        {
            Id = id;
            Email = email;
            CompletoPedido = completoPedido;
        }
    }
}