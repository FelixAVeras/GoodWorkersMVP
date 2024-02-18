using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace GoodWorkersMVP.Services
{
    public class OcupationService
    {
        private readonly ApiService apiService;

        public OcupationService()
        {
            apiService = new ApiService(); // Asegúrate de inicializar tu servicio API según tus necesidades
        }

        public async Task<List<User>> LoadUsersByOcupation(int ocupationId)
        {
            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    connection.Message,
                    Languages.BtnAcceptDialog);

                await Application.Current.MainPage.Navigation.PopAsync();
                return null;
            }

            var response = await apiService.GetList<User>(
                "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
                "api/",
                $"ocupations/{ocupationId}/users");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return null;
            }

            return (List<User>)response.Result;
        }
    }
}
