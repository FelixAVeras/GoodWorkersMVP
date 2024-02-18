using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.ViewModels;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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

            if (Settings.IsRemember && string.IsNullOrEmpty(Settings.AccessToken))
            {
                MainViewModel.GetInstance().Ocupations = new OcupationViewModel();

                MainPage = new MasterPage();
            }
            else
            {
                MainViewModel.GetInstance().Login = new LoginViewModel();

                MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = Color.FromArgb("#253544")
                };
            }
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
