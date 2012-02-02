using System;
using System.Configuration;

namespace Informedica.GenForm.Settings
{
    public class ConfigurationManagerSettingWriter : SettingWriter
    {
        public override void WriteSetting(string key, string value)
        {
            if (String.IsNullOrWhiteSpace(key) || String.IsNullOrWhiteSpace(value)) return;

            value = EncryptSetting(value);
            ConfigurationManager.AppSettings.Set(key, value);
        }

        private string EncryptSetting(string value)
        {
            return new SymCryptography().Encrypt(value);
        }
    }
}