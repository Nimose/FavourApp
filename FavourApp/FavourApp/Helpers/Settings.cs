// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace FavourApp.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
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

        private const string AccessTokenKey = "accessTokenKey";
        private static readonly string SettingsDefault = string.Empty;


        private const string FacebookIdKey = "facebookId";
        private static readonly string SettingsDefault_ = string.Empty;


        #endregion

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccessTokenKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccessTokenKey, value);
            }
        }
        public static void RemoveAccessToken() => AppSettings.Remove(nameof(AccessToken));


        public static string FacebookId
        {
            get
            {
                return AppSettings.GetValueOrDefault(FacebookIdKey, SettingsDefault_);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FacebookIdKey, value);
            }
        }
        public static void RemoveFacebookId() => AppSettings.Remove(nameof(FacebookId));


        public static void ClearEverything()
        {
            AppSettings.Clear();
        }
    }
}