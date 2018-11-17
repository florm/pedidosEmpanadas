using App.Models;
using Newtonsoft.Json;
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
                string url = string.Format("/api/Usuarios/GetListaPedidos/{0}", id);
                string url2 = "http://192.168.0.6:45457" + url;
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
    }
}