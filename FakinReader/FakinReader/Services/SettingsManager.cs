using Xamarin.Essentials;

namespace FakinReader.Services
{
    public class SettingsManager : ISettingsManager
    {
        public void SaveSetting(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public string GetSetting(string key)
        {
            return Preferences.Get(key, null);
        }
    }
}