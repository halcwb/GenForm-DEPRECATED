using System.Configuration;
using Informedica.GenForm.Settings.ConfigurationSettings;

namespace Informedica.GenForm.Settings.Tests.SettingsManagement
{
    public class TestConfigurationFactory: ConfigurationFactory
    {
        private Configuration _config;

        public TestConfigurationFactory(Configuration config)
        {
            _config = config;
        }

        #region Overrides of ConfigurationFactory

        public override Configuration GetConfiguration()
        {
            return _config;
        }

        #endregion
    }
}