using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.ViewModels;
using Newtonsoft.Json;
using System;
using System.Windows.Input;

namespace GoodWorkersMVP.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("cellphone")]
        public string Cellphone { get; set; }

        [JsonProperty("profile_image")]
        public string ProfileImage { get; set; }

        [JsonProperty("birthday")]
        public DateTimeOffset Birthday { get; set; }

        [JsonProperty("age")]
        public long Age { get; set; }

        [JsonProperty("Document_number")]
        public string DocumentNumber { get; set; }

        [JsonProperty("about_me")]
        public string AboutMe { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("document_type_id")]
        public long DocumentTypeId { get; set; }

        [JsonProperty("ocupation_id")]
        public long OcupationId { get; set; }

        [JsonProperty("userType_id")]
        public long UserTypeId { get; set; }


        public Ocupation Ocupation { get; set; }
        public int OcupationID { get; set; }

        public string ImageFullPath
        {
            get
            {
                return string.Format("https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/{0}", ProfileImage);
            }
        }

        public ICommand SelectUserCommand => new RelayCommand(SelectUser);

        void SelectUser()
        {
            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.User = this;
            mainViewModel.Profile = new ProfileViewModel(this);
        }
    }
}
