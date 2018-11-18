using App.Models;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupDetalleAcciones : PopupPage
	{
		public PopupDetalleAcciones (PedidosVm pedido)
		{
			InitializeComponent ();

            if(pedido.Estado != 1)
            {
                ImgOk.IsVisible = false;
                ImgEdit.IsVisible = false;
                ImgDelete.IsVisible = false;
                FramePopUp.BackgroundColor = Color.LightGray;
            }

            FechaCreacion.Text = "Fecha: " + pedido.FechaCreacion;
            NombreNegocio.Text = "Negocio: " + pedido.NombreNegocio;
            EstadoS.Text = "Estado de pedido: " + pedido.EstadoS;
            RolS.Text = "Rol: " + pedido.RolS;
            Descripcion.Text = pedido.Descripcion;
            PrecioDocena.Text = "Precio por docena: $ " + pedido.PrecioDocena;
            PrecioUnidad.Text = "Precio por unidad: $ " + pedido.PrecioUnidad;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            List<DeviceUser> ListaDePrueba = new List<DeviceUser>();
            ListaDePrueba.Add(new DeviceUser { Email = "Pepex", IdUsuario = 1 });
            ListaDePrueba.Add(new DeviceUser { Email = "Poli", IdUsuario = 2 });
            ListaDePrueba.Add(new DeviceUser { Email = "Lele", IdUsuario = 3 });
            ListaDePrueba.Add(new DeviceUser { Email = "Nene", IdUsuario = 45 });

            await Navigation.PushAsync(new Borrar(ListaDePrueba));
        }

        private async void IrAElegirGustos(object sender, EventArgs e)
        {
            int.TryParse(((Image)sender).ClassId, out int idPedido);
            await Navigation.PushAsync(new ElegirGustos(idPedido));
        }
    }
}