using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class VacancyViewModel : BaseViewModel
    {
        ApiService apiservice;

        public ICommand NewOfferCommand => new RelayCommand(AddNewOfferForm);

        public VacancyViewModel()
        {
            apiservice = new ApiService();
        }

        async void AddNewOfferForm()
        {
            MainViewModel.GetInstance().AddVacancy = new AddVacancyViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddVacancyPage());
        }
    }
}
