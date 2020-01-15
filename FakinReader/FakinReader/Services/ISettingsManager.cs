namespace FakinReader.Services
{
    public interface ISettingsManager
    {
        #region Methods
        string GetSetting(string key);
        void SaveSetting(string key, string value);
        #endregion Methods
    }
}