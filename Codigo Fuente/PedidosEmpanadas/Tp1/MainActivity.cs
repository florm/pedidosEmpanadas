using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;

namespace Tp1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public List<string> Items;
        public ListView Lista;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Items = new List<string>();
            Items.Add("item1");
            Items.Add("item2");
            Items.Add("item3");
            Items.Add("item4");

            Lista = FindViewById<ListView>(Resource.Id.lista);
            ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Items);
            Lista.Adapter = adaptador;
            //// Set our view from the "main" layout resource
        }
    }
}