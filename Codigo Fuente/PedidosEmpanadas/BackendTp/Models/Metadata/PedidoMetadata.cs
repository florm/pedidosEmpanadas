using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackendTp.Models.Metadata
{
    public class PedidoMetadata
    {
        [Required(ErrorMessage = "Debe completar el nombre del negocio")]
        [StringLength(200, ErrorMessage = "El nombre del negocio no puede superar los 200 caracteres")]
        public string NombreNegocio { get; set; }
        [Required(ErrorMessage = "Debe completar la descripción")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Debe completar el precio por unidad")]
        public int PrecioUnidad { get; set; }
        [Required(ErrorMessage = "Debe completar el precio por docena")]
        public int PrecioDocena { get; set; }
        [Required(ErrorMessage = "Debe ingresar invitados")]
        public virtual ICollection<InvitacionPedido> InvitacionPedido { get; set; }
        [Required]
        public virtual ICollection<GustoEmpanada> GustoEmpanada { get; set; }
    }
}