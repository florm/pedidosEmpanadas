using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackendTp.Models.Metadata
{
    public class UsuarioMetadata
    {
        [Required(ErrorMessage = "Debe completar su mail")]
        [StringLength(300, ErrorMessage = "El mail no puede superar los 300 caracteres")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe completar la contraseña")]
        [StringLength(50, ErrorMessage = "La contraseña no puede superar los 50 caracteres")]
        public string Password { get; set; }
    }
}