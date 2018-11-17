using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Correo.Text) || string.IsNullOrEmpty(Pass.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un usuario y una contraseña", "Aceptar");
                return;
            }

            ActIndicator.IsRunning = true;
            string result;

            try
            {
                Boton.IsEnabled = false;
                HttpClient client = new HttpClient();

                string url = string.Format("/api/Usuarios/GetMobileUser/{0}/{1}", Correo.Text, Pass.Text);

                string url2 = "http://192.168.0.6:45457" + url;

                var response = await client.GetAsync(url2);
                result = response.Content.ReadAsStringAsync().Result;
                Boton.IsEnabled = true;
            }
            catch
            {
                ActIndicator.IsRunning = false;
                await DisplayAlert("Error", "Error de conexion", "Aceptar");
                Boton.IsEnabled = true;
                return;

            }

            ActIndicator.IsRunning = false;

            if (string.IsNullOrEmpty(result) || result == "null")
            {
                await DisplayAlert("Error", "Usuario o contraseña no valido", "Aceptar");
                Pass.Text = string.Empty;
                Pass.Focus();
                Boton.IsEnabled = true;
                return;
            }

            var deviceUser = JsonConvert.DeserializeObject<DeviceUser>(result);

            App.Current.Properties["IdUsuario"] = deviceUser.IdUsuario; 
            App.Current.Properties["IsLoggedIn"] = true;

            //await Navigation.PushAsync(new Lista(deviceUser));
            await Navigation.PushAsync(new Lista());
        }

        private async void IrARegistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrar());
        }
    }
}
