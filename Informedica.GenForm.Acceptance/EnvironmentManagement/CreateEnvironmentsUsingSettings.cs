using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class CreateEnvironmentsUsingSettings
    {
        private readonly ICollection<ISetting> _source = new TestSource();
        private EnvironmentSettingsCollection _settings;
        private GenFormEnvironmentCollection _environments;

        public string CreateSettingWithKeyAndValue(string key, string value)
        {
            try
            {
                _source.Add(SettingFactory.CreateSecureSetting<ConnectionStringSettings>(key, value));
                return "Success";
            }
            catch (System.Exception e)
            {
                return e.ToString();
            }
        }

        public bool ListOfEnvironmentsContainsForMachine(string environment, string machine)
        {
            try
            {
                _settings = new EnvironmentSettingsCollection(_source);
                var envs = new EnvironmentCollection(_settings);
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
            return _source.Any(s => s.Key == key && s.Value == value);
        }

        public bool ClearSettingList()
        {
            ((TestSource)_source).Clear();
            return true;
        }
    }
}
