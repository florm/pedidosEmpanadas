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

        private readonly ServicioGustoEmpanada _servicioGustoEmpanada = new ServicioGustoEmpanada();
        public decimal PrecioTotal => CalcularPrecioTotal();
        public decimal PrecioCalculadoPorUnidad => CalcularPrecioPorUnidad();


        public decimal CalcularPrecioTotal()
        {

            int CantidadTotal = _servicioGustoEmpanada.CantidadTotalDeEmpanadas(IdPedido);
            decimal precio = PrecioUnidad;
            int cantidadDocenas = CantidadTotal / 12;
            int empanadasSobrantes = CantidadTotal % 12;

            try
            {
                decimal PrecioTotal = (cantidadDocenas * PrecioDocena) + (empanadasSobrantes * PrecioUnidad);
                return Math.Round(PrecioTotal);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        private decimal CalcularPrecioPorUnidad()
        {
            int CantidadTotal = _servicioGustoEmpanada.CantidadTotalDeEmpanadas(IdPedido);
            try
            {
                decimal PrecioPorUnidad = PrecioTotal / CantidadTotal;
                return Math.Round(PrecioPorUnidad, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}