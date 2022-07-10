
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoodWorkersMVP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage
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