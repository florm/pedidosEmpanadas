using App.Models;
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
	public partial class Borrar : ContentPage
	{
        List<Entry> TestListaEntries = new List<Entry>();
        List<DeviceUser> ListaUsuarios;
		public Borrar (List<DeviceUser> Lista)
		{
			InitializeComponent ();
            ListaUsuarios = Lista;
            
            foreach(DeviceUser u in Lista)
            {
                //ContenedorEntrys.Children.Add(new Entry() { Text = u.Email, ClassId = u.IdUsuario.ToString() });
                TestListaEntries.Add(new Entry() { Text = u.Email, ClassId = u.IdUsuario.ToString() });
            }

            foreach(Entry entr in TestListaEntries)
            {
                ContenedorEntrys.Children.Add(entr);
            }          

		}

        private void BotonReg_Clicked(object sender, EventArgs e)
        {
            List<string> testlist = new List<string>();

            foreach (Entry ent in TestListaEntries)
            {
                testlist.Add(ent.Text);
            }

            foreach (string ent in testlist)
            {
                ContenedorEntrys.Children.Add(new Label() { Text = ent});
            }
            //foreach (DeviceUser u in ListaUsuarios)
            //{
            //    testlist.Add(((Entry)sender).Text);
            //}

            var eeee = 45 - 3;
        }
    }
}