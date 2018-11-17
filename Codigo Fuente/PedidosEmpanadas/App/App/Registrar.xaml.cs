using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registrar : ContentPage
	{
		public Registrar ()
		{
			InitializeComponent ();
		}

        private async void Registrar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(CorreoRegistro.Text) || string.IsNullOrEmpty(PassRegistro.Text) || string.IsNullOrEmpty(RePassRegistro.Text))
            {
                await DisplayAlert("Error", "Todos los campos deben estar completos.", "Aceptar");
                return;
            }

            if (!Regex.Match(CorreoRegistro.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                await DisplayAlert("Error", "Correo invalido.", "Aceptar");
                return;
            }

            if (PassRegistro.Text != RePassRegistro.Text)
            {
                await DisplayAlert("Error", "Las contraseñas deben coincidir.", "Aceptar");
                PassRegistro.Text = string.Empty;
                RePassRegistro.Text = string.Empty;
                return;
            }

            ActIndicator.IsRunning = true;

            var usuario = new UserReg()
            {
                Email = CorreoRegistro.Text,
                Password = PassRegistro.Text
            };

            var jsonRequest = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

            string result;

            try
            {
                BotonReg.IsEnabled = false;
                HttpClient client = new HttpClient();

                string url = string.Format("/api/Usuarios/PostNewUser");
                string url2 = "http://192.168.0.6:45457" + url;

                var response = await client.PostAsync(url2, content);
                result = response.Content.ReadAsStringAsync().Result;
                BotonReg.IsEnabled = true;
            }
            catch
            {
                ActIndicator.IsRunning = false;
                await DisplayAlert("Error", "Error de conexion", "Aceptar");
                BotonReg.IsEnabled = true;
                return;
            }

            ActIndicator.IsRunning = false;

            if (result == "error")
            {
                await DisplayAlert("Error", "Ya existe un usuario con el email " + CorreoRegistro.Text + ".", "Aceptar");
                CorreoRegistro.Focus();
                BotonReg.IsEnabled = true;
                return;
            }

            await DisplayAlert("Alerta", "Registro exitoso!", "Aceptar");
            await Navigation.PushAsync(new MainPage());
        }

    }
}