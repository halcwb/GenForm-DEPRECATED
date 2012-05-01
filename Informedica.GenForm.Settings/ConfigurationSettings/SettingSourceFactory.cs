using System.Collections.Generic;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.ConfigurationSettings
{
    public class SettingSourceFactory
    {
        public static ICollection<ISetting> GetSettingSource()
        {
            return new ConfigurationSettingSource(new WebConfigurationFactory());
        }
    }
}