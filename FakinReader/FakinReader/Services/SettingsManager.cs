using Xamarin.Essentials;

namespace FakinReader.Services
{
    public class SettingsManager : ISettingsManager
    {
        #region Methods
        public string GetSetting(string key)
        {
            return Preferences.Get(key, null);
        }
        public void SaveSetting(string key, string value)
        {
            Preferences.Set(key, value);
        }
        #endregion Methods
    }
}