using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App
{
    public partial class App : Application
    {
        public static App Current;
        public static string UrlApi;
        public App()
        {
            InitializeComponent();
            Current = this;
            UrlApi = "http://192.168.0.6:45455"; 
            //var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;
            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false;
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
