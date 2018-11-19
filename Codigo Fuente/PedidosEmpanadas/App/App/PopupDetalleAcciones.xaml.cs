using App.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        private async void IrAEliminar(object sender, EventArgs e)
        {
            string result;
            try
            {
                HttpClient client = new HttpClient();
                string url = string.Format("/api/Pedidos/EliminarPedido/{0}", Pedido.IdPedido);
                string url2 = App.UrlApi + url;
                //var jsonRequest = JsonConvert.SerializeObject(Pedido.IdPedido);
                //var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");
                var response = await client.DeleteAsync(url2);
                //var response = await client.PostAsync(url2, content);
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                await DisplayAlert("Error", "Error de conexion", "Aceptar");
                return;
            }

            //string respuesta = JsonConvert.DeserializeObject<string>(result);
            //await DisplayAlert("", respuesta, "Aceptar");

        }

        private async void IrAElegirGustos(object sender, EventArgs e)
        {
            
            //await PopupNavigation.Instance.PopAsync(true);
            //int.TryParse(((Image)sender).ClassId, out int idPedido);
            //int idPedido = 1;
            await Navigation.PushAsync(new ElegirGustos(Pedido));
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}