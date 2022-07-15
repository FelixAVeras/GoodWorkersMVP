using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using GoodWorkersMVP.Helpers;

namespace GoodWorkersMVP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapsPage : ContentPage
	{
		public MapsPage ()
		{
			InitializeComponent ();

            GetPositionCommand = new Command(async () => await OnGetPosition());

            BindingContext = this;
        }

        public ICommand GetPositionCommand { get; }
        string geocodePosition;

        public string GeocodePosition
        {
            get => geocodePosition;
            set => SetProperty(ref geocodePosition, value);
        }

        async Task OnGetPosition()
        {
            try
            {
                var address = addressEntry.Text;
                var locations = await Geocoding.GetLocationsAsync(address);

                Location location = locations.FirstOrDefault();

                if (location == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.UnableDetecLocation,
                    Languages.BtnAcceptDialog);

                    return;
                }
                else
                {
                    latitudeEntry.Text = $"{location.Latitude}";
                    longitudeEntry.Text = $"{location.Longitude}";
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.UnableDetecLocation + ": " + ex.Message,
                    Languages.BtnAcceptDialog);

                return;
            }
        }

        private async void btnGoToMap_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(latitudeEntry.Text, out double lat)) return;
            if (!double.TryParse(longitudeEntry.Text, out double lng)) return;

            await Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                Name = addressEntry.Text,
                NavigationMode = NavigationMode.None
            });
        }

        private async void btnGetLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(10)
                    });
                }

                if (location != null)
                {
                    latitudeEntry.Text = $"{location.Latitude}";
                    longitudeEntry.Text = $"{location.Longitude}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Algo salio mal: {ex.Message}");
            }
        }

        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}