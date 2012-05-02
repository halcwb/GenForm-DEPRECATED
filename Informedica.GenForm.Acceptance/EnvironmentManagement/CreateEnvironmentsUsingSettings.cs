using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class CreateEnvironmentsUsingSettings
    {
        private const string Success = "Success";
        private readonly ICollection<ISetting> _source = new TestSource();
        private EnvironmentSettingsCollection _settings;
        private GenFormEnvironmentCollection _environments;
        private const string Fail = "Fail";

        public string CreateSettingWithKeyAndValue(string key, string value)
        {
            try
            {
                _source.Add(SettingFactory.CreateSecureSetting<ConnectionStringSettings>(key, value));
                return Success;
            }
            catch (System.Exception e)
            {
                return e.ToString();
            }
        }

        public string ListOfEnvironmentsContainsForMachine(string environment, string machine)
        {
            try
            {
                RefreshEnvironments();

                return _environments.Any(e => e.Name == environment && e.MachineName == machine) ? Success : Fail;
            }
            catch (System.Exception e)
            {
                return e.ToString();
            }
        }

        private void RefreshEnvironments()
        {
            _settings = new EnvironmentSettingsCollection(_source);
            var envs = new EnvironmentCollection(_settings);
            _environments = new GenFormEnvironmentCollection(envs);
        }

        public string CheckEnvironmentIfHasSettingWithValue(string environment, string setting, string value)
        {
            try
            {
                RefreshEnvironments();

                if (setting.ToLower() == "database")
                    return (_environments.Single(e => e.Name == environment).Database == value).ToString();

                if (setting.ToLower() == "logpath")
                    return (_environments.Single(e => e.Name == environment).LogPath == value).ToString();

                if (setting.ToLower() == "exportpath")
                    return (_environments.Single(e => e.Name == environment).ExportPath == value).ToString();
                
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

        public string AddEnvironmentToMachineWithDatabaseLogPathExportPath(string environment, string machine, string database, string logpath, string exportpath)
        {
            try
            {
                _environments.Add(EnvironmentFactory.GetGenFormEnvironment(machine, environment, "provider", database, logpath, exportpath));
                return Success;
            }
            catch (System.Exception e)
            {
                return e.ToString();
            }
        }

        public bool ClearSettingList()
        {
            ((TestSource)_source).Clear();
            return true;
        }
    }
}
