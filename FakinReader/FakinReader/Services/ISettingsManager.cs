namespace FakinReader.Services
{
    public interface ISettingsManager
    {
        string GetSetting(string key);
        void SaveSetting(string key, string value);
    }
}