
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace GoodWorkersMVP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : FlyoutPage
    {
		public MasterPage ()
		{
			InitializeComponent ();

            App.Navigator = Navigator;
            App.Master = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}