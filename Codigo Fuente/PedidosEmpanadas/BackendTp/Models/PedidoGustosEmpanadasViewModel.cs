using BackendTp.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackendTp.Models
{
    public class PedidoGustosEmpanadasViewModel
    {
        public Pedido Pedido { get; set; }
        public List<GustosEmpanadasViewModel> GustosDisponibles{ get; set; }
        
        public List<Usuario> Invitados { get; set; }

        public PedidoGustosEmpanadasViewModel()
        {
            GustosDisponibles = new List<GustosEmpanadasViewModel>();
            Invitados = new List<Usuario>();
            
        }

        public PedidoGustosEmpanadasViewModel(Pedido pedido, List<GustoEmpanada> gustosPedido, 
            List<GustoEmpanada> gustosModel, List<Usuario> invitados)
        {
            Pedido = pedido;
            GustosDisponibles = new List<GustosEmpanadasViewModel>();
            Invitados = invitados;
            foreach (var gusto in gustosModel)
            {
                if(gustosPedido.Any(g=> g.IdGustoEmpanada == gusto.IdGustoEmpanada))
                GustosDisponibles.Add(new GustosEmpanadasViewModel
                    { Id = gusto.IdGustoEmpanada, Nombre = gusto.Nombre, IsSelected = true });
                else
                GustosDisponibles.Add(new GustosEmpanadasViewModel
                    { Id = gusto.IdGustoEmpanada, Nombre = gusto.Nombre, IsSelected = false });

            }

        }

    }
}