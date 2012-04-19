using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.ConfigurationSettings
{
    public class SettingSourceFactory
    {
        public static ISettingSource GetSettingSource()
        {
            return new WebConfigSettingSource();
        }

        public static ISettingSource GetSecureSource()
        {
            return new SecureSettingSource(GetSettingSource());
        }
    }
}