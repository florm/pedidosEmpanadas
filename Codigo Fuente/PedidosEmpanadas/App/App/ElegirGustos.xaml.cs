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
	public partial class ElegirGustos : ContentPage
	{
        public int IdPedido { get; set; }
        List<GustoEmpanadasCantidad> ListaDeGustos = new List<GustoEmpanadasCantidad>();
        List<Entry> ListaEntries = new List<Entry>();
        List<Label> ListaLabels = new List<Label>();

        public ElegirGustos(PedidosVm pedido)
        {
            InitializeComponent();

            NombreNegocio.Text = "Negocio: " + pedido.NombreNegocio;
            if (pedido.Descripcion != null || pedido.Descripcion != "")
            {
                Descripcion.Text = pedido.Descripcion;
            }
            else
            {
                Descripcion.IsVisible = false;
            }
            PrecioDocena.Text = "Precio por docena: $ " + pedido.PrecioDocena;
            PrecioUnidad.Text = "Precio por unidad: $ " + pedido.PrecioUnidad;

            this.InicializarEntrys(pedido.IdPedido);
            IdPedido = pedido.IdPedido;
        }

        public async void InicializarEntrys(int id)
        {
            string result;
            try
            {
                HttpClient client = new HttpClient();
                string url = string.Format("/api/Pedidos/GetListaDeGustosEnPedido/{0}/{1}", id, App.Current.Properties["IdUsuario"]);
                string url2 = App.UrlApi + url;
                var response = await client.GetAsync(url2);
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                await DisplayAlert("Error", "Error de conexion", "Aceptar");
                return;
            }

            ListaDeGustos = JsonConvert.DeserializeObject<List<GustoEmpanadasCantidad>>(result);

            foreach (GustoEmpanadasCantidad u in ListaDeGustos)
            {
                ListaEntries.Add(new Entry() { ClassId = u.IdGustoEmpanada.ToString(), Text = u.Cantidad.ToString(), Keyboard = Keyboard.Numeric });
                ListaLabels.Add(new Label() { Text = u.Nombre, ClassId = u.IdGustoEmpanada.ToString() });
            }

            foreach (Entry entr in ListaEntries)
            {
                foreach (Label lb in ListaLabels)
                {
                    if (entr.ClassId == lb.ClassId)
                    {
                        ContenedorEntrys.Children.Add(lb);
                    }
                }
                ContenedorEntrys.Children.Add(entr);
            }
        }

        private async void BotonReg_Clicked(object sender, EventArgs e)
        {
            string txt = "";
            foreach (Entry entr in ListaEntries)
            {
                txt = entr.Text;
                if (txt.Contains("."))
                {
                    await DisplayAlert("", "Solo se admiten números enteros", "Ok");
                    return;
                }
            }

            PedidoRequest pr = new PedidoRequest();
            foreach (Entry entr in ListaEntries)
            {
                foreach (GustoEmpanadasCantidad g in ListaDeGustos)
                {
                    if (g.IdGustoEmpanada.ToString() == entr.ClassId)
                    {
                        int cant;
                        int.TryParse(entr.Text, out cant);
                        g.Cantidad = cant;
                    }
                }
            }

            pr.GustoEmpanadasCantidad = ListaDeGustos;
            pr.IdPedido = IdPedido;
            //int id;
            int.TryParse(App.Current.Properties["IdUsuario"].ToString(), out int id);
            pr.IdUsuario = id;

            var jsonRequest = JsonConvert.SerializeObject(pr);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

            string result;

            try
            {
                HttpClient client = new HttpClient();

                string url = string.Format("/api/Pedidos/ConfirmarGustos");
                string url2 = App.UrlApi + url;

                var response = await client.PostAsync(url2, content);
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                await DisplayAlert("Error", "Error de conexion", "Aceptar");
                return;
            }


            var des = JsonConvert.DeserializeObject<string>(result);
            await DisplayAlert("", des, "ok");
            await Navigation.PushAsync(new Lista());
        }
    }
}