using Xamarin.Essentials;

namespace FakinReader.Helpers
{
    public class SettingsManager
    {
        public static void SaveSetting(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public static string GetSetting(string key)
        {
            return Preferences.Get(key, null);
        }
    }
}