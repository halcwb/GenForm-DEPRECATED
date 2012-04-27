using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;

namespace Informedica.GenForm.Settings.Environments
{
    public class Environment
    {
        private EnvironmentSettingsCollection _settings;

        private static EnvironmentSettingsCollection CreateEnvironmentSettings(string machine, string environment)
        {
            return new EnvironmentSettingsCollection(machine, environment, SettingSourceFactory.GetSecureSettingSource());
        }

        public Environment(string machine, string name, EnvironmentSettingsCollection settings)
        {
            Init(name, machine, settings);
        }

        public Environment(string name, string machine)
        {
            Init(name, machine, CreateEnvironmentSettings(machine, name));
        }

        private void Init(string environmentName, string machineName, EnvironmentSettingsCollection settings)
        {
            Name = environmentName;
            MachineName = machineName;
            _settings = settings;
        }

        public string Name { get; private set; }

        public IEnumerable<EnvironmentSetting> Settings
        {
            get { return _settings.Where(s => s.MachineName == MachineName && s.Environment == Name).ToList(); }
        }

        public string MachineName { get; private set; }

        public static Environment Create(string name, string machine)
        {
            return new Environment(name, machine);
        }

        public void AddSetting(string name, string provider, string connectionString)
        {
            _settings.AddSetting(name, provider, connectionString);
        }

        public void AddSetting(string name, string provider)
        {
            AddSetting(name, provider, string.Empty);
        }
    }
}