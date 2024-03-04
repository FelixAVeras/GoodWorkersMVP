using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace GoodWorkersMVP.Helpers
{
    public static class Settings
    {
        private const string tokenType = "TokenType";
        private const string accessToken = "AccessToken";
        private const string isRemember = "IsRemember";

        private const string token = "Token";
        
        private static readonly string stringDefault = string.Empty;
        private static readonly bool booleanDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(token, stringDefault);
            set => AppSettings.AddOrUpdateValue(token, value);
        }

        public static string TokenType
        {
            get => AppSettings.GetValueOrDefault(tokenType, stringDefault);
            set => AppSettings.AddOrUpdateValue(tokenType, value);
        }

        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault(accessToken, stringDefault);
            set => AppSettings.AddOrUpdateValue(accessToken, value);   
        }

        public static bool IsRemember
        {
            get => AppSettings.GetValueOrDefault(isRemember, booleanDefault);
            set => AppSettings.AddOrUpdateValue(isRemember, value);
        }
    }
}
