using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class CreateEnvironmentsUsingSettings
    {
        private ICollection<ISetting> _settings = new TestSource();
        private EnvironmentSettingsCollection _envSettings;
        private GenFormEnvironmentCollection _environments;

        public void CreateSettingWithKeyAndValue (string key, string value)
        {
            _settings.Add(SettingFactory.CreateSecureSetting(new ConnectionStringSettings()));
        }

        public bool ListOfEnvironmentsContainsForMachine(string environment, string machine)
        {
            try
            {
                var envs = new EnvironmentCollection(_envSettings);
                _environments = new GenFormEnvironmentCollection(envs);

                return _environments.Any(e => e.Name == environment && e.MachineName == machine);
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public string CheckEnvironmentIfHasSettingWithValue(string environment, string setting, string value)
        {
            try
            {
                if (setting.ToLower() == "database")
                    return (_environments.SingleOrDefault(e => e.Name == environment).Database == value).ToString();

                return false.ToString();

            }
            catch (System.Exception e)
            {
                return e.ToString();
            }
        }

        public bool ChangeSettingOfEnvironmentOnMachineTo(string setting, string environment, string machine, string value)
        {
            var env = _environments.SingleOrDefault(e => e.Name == environment && e.MachineName == machine);
            if (setting.ToLower() == "database") env.Database = value;

            return _environments.SingleOrDefault(e => e.Name == environment && e.MachineName == machine).Database == value;
        }

        public bool CheckIfSettingWithKeyAndValueExists(string key, string value)
        {
            return _settings.Any(s => s.Key == key && s.Value == value);
        }

        public bool ClearSettingList()
        {
            ((TestSource)_settings).Clear();
            return true;
        }
    }
}
