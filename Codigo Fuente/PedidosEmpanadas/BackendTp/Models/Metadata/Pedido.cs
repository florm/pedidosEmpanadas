using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BackendTp.Models.Metadata;
using BackendTp.Servicios;

namespace BackendTp.Models
{
    [MetadataType(typeof(PedidoMetadata))]
    public partial class Pedido
    {
        private static readonly Entities Context = new Entities();
        private readonly ServicioGustoEmpanada _servicioGustoEmpanada = new ServicioGustoEmpanada(Context);
        public decimal PrecioTotal => CalcularPrecioTotal();
        public decimal PrecioCalculadoPorUnidad => CalcularPrecioPorUnidad();

        public decimal CalcularPrecioTotal()
        {

            int cantidadTotal = _servicioGustoEmpanada.CantidadTotalDeEmpanadas(IdPedido);
            //decimal precio = PrecioUnidad;
            int cantidadDocenas = cantidadTotal / 12;
            int empanadasSobrantes = cantidadTotal % 12;

            try
            {
                decimal precioTotal = (cantidadDocenas * PrecioDocena) + (empanadasSobrantes * PrecioUnidad);
                return Math.Round(precioTotal);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private decimal CalcularPrecioPorUnidad()
        {
            int cantidadTotal = _servicioGustoEmpanada.CantidadTotalDeEmpanadas(IdPedido);
            try
            {
                decimal precioPorUnidad = PrecioTotal / cantidadTotal;
                return Math.Round(precioPorUnidad, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}