using System.Configuration;

namespace Informedica.Settings
{
    public class ConfigurationManagerSettingReader : SettingReader   
    {
        public override string ReadSetting(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            return new SymCryptography().Decrypt(value);
        }
    }
}