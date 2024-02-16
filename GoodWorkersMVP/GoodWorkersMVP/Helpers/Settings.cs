using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace GoodWorkersMVP.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string tokenType = "TokenType";
        private const string accessToken = "AccessToken";
        private const string isRemember = "IsRemember";
        
        private static readonly string stringDefault = string.Empty;
        private static readonly bool booleanDefault = false;

        #endregion


        public static string TokenType
        {
            get
            {
                return AppSettings.GetValueOrDefault(tokenType, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(tokenType, value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(accessToken, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(accessToken, value);
            }
        }

        public static bool IsRemember
        {
            get
            {
                return AppSettings.GetValueOrDefault(isRemember, booleanDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isRemember, value);
            }
        }
    }
}
