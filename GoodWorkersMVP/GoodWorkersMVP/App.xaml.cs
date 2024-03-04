using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models.ModelResponse;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.ViewModels;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GoodWorkersMVP
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();

            if (Settings.IsRemember)
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                
                MainViewModel.GetInstance().Ocupations = new OcupationViewModel();
                    
                MainViewModel.GetInstance().Token = token;

                MainPage = new MasterPage();

                return;
            }
            
            MainViewModel.GetInstance().Login = new LoginViewModel();

            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromHex("#253544")
            };
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
    }
}
