using App.Models;
using Newtonsoft.Json;
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
	public partial class Lista : ContentPage
	{
        public List<PedidosVm> ListaDePedidos;
        //public Lista(DeviceUser deviceUser)
        public Lista ()
		{
			InitializeComponent ();
            var idUsuarioStr = App.Current.Properties["IdUsuario"].ToString();
            //int idUsuario;
            int.TryParse(idUsuarioStr, out int idUsuario);
            //EmailUser.Text = string.Format("Bienvenido {0}", deviceUser.Email);
            //this.cargarPedidos(deviceUser.IdUsuario);
            this.cargarPedidos(idUsuario);
        }

        public async void cargarPedidos(int id)
        {
            string result;
            try
            {
                HttpClient client = new HttpClient();
                string url = string.Format("/api/Pedidos/GetListaPedidos/{0}", id);
                string url2 = App.UrlApi + url;
                var response = await client.GetAsync(url2);
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                await DisplayAlert("Error", "Error de conexion", "Aceptar");
                return;
            }

            ListaDePedidos = JsonConvert.DeserializeObject<List<PedidosVm>>(result);
            ListaPruebaAle.ItemsSource = ListaDePedidos;
        }

        private async void MasAcciones(object sender, EventArgs e)
        {
            var boton = (Image)sender;
            var IdPedidoStr = boton.ClassId;
            int.TryParse(IdPedidoStr, out int IdPedido);
            PedidosVm pedidoCompleto = new PedidosVm();

            foreach(PedidosVm p in ListaDePedidos)
            {
                if(p.IdPedido == IdPedido)
                {
                    pedidoCompleto = p;
                }
            }

            await PopupNavigation.Instance.PushAsync(new PopupDetalleAcciones(pedidoCompleto));
        }

        private void LogOutApp(object sender, EventArgs e)
        {
            App.Current.Logout();
        }
    }
}