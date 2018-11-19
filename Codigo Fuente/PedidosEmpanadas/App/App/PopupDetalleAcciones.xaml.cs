using App.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupDetalleAcciones : PopupPage
	{
        PedidosVm Pedido = new PedidosVm();
        public PopupDetalleAcciones(PedidosVm pedido)
        {
            InitializeComponent();

            if (pedido.Estado != 1)
            {
                ImgOk.IsVisible = false;
                ImgEdit.IsVisible = false;
                ImgDelete.IsVisible = false;
                FramePopUp.BackgroundColor = Color.LightGray;
            }

            Pedido = pedido;
            FechaCreacion.Text = "Fecha: " + pedido.FechaCreacion;
            NombreNegocio.Text = "Negocio: " + pedido.NombreNegocio;
            EstadoS.Text = "Estado de pedido: " + pedido.EstadoS;
            RolS.Text = "Rol: " + pedido.RolS;
            Descripcion.Text = pedido.Descripcion;
            PrecioDocena.Text = "Precio por docena: $ " + pedido.PrecioDocena;
            PrecioUnidad.Text = "Precio por unidad: $ " + pedido.PrecioUnidad;
        }

        private async void IrAElegirGustos(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ElegirGustos(Pedido));
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}