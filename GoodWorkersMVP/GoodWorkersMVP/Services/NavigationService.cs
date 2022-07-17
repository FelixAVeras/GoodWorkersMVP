
using GoodWorkersMVP.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoodWorkersMVP.Services
{
    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginPage":
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;

                case "MasterPage":
                    Application.Current.MainPage = new MasterPage();
                    break;
            }
        }

        public void NavigateOnLogin(string pageName)
        {
            switch (pageName)
            {
                case "RegisterPage":
                    Application.Current.MainPage = new NavigationPage(new RegisterPage());
                    break;

                //case "RecoverPasswordPage":
                //    Application.Current.MainPage = new NavigationPage(new RecoverPasswordPage());
                //    break;
            }
        }

        public async Task NavigateOnMaster(string pageName)
        {
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "OcupationPage": await App.Navigator.PushAsync(new OcupationPage());
                        break;

                case "UserPage": await App.Navigator.PushAsync(new UsersPage());
                    break;
            }
        }

        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }
    }
}
