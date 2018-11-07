using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BackendTp.Models.Metadata;

namespace BackendTp.Models
{
    [MetadataType(typeof(PedidoMetadata))]
    public partial class Pedido
    {

        public decimal PrecioTotal => CalcularPrecioTotal();
        public decimal PrecioPorUnidad => CalcularPrecioPorUnidad();

        private decimal CalcularPrecioPorUnidad()
        {
            return 1;
        }

        private decimal CalcularPrecioTotal()
        {
            return 10;
        }
    }
}