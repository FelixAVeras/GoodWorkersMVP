using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GoodWorkersMVP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoodWorkersMVP.Droid
{
    //[assembly: Dependency(typeof(NavigationServiceAndroid))]
    public class NavigationServiceAndroid : INavigationService
    {
        public async Task NavigateToAsync(Page page)
        {
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}