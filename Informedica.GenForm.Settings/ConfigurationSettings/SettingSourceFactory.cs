using System;
using System.Configuration;
using System.Web.Configuration;
using Informedica.SecureSettings.Cryptographers;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.ConfigurationSettings
{
    public class SettingSourceFactory
    {
        public static SecureSettingSource GetSecureSettingSource()
        {
            ConfigurationFactory factory = new WebConfigurationFactory();
            var source = new ConfigurationSettingSource(factory);
            var keyMan = new SecretKeyManager();
            return new SecureSettingSource(source, keyMan, CryptographyFactory.GetCryptographt());
        }
    }

    public class WebConfigurationFactory : ConfigurationFactory
    {
        #region Overrides of ConfigurationFactory

        public override Configuration GetConfiguration()
        {
            return WebConfigurationManager.OpenWebConfiguration("/GenForm");
        }

        #endregion
    }
}