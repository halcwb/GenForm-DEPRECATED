using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;

namespace Informedica.GenForm.Settings.Environments
{
    public class Environment
    {
        private ICollection<EnvironmentSetting> _settings;

        private static EnvironmentSettingsCollection CreateEnvironmentSettings()
        {
            return new EnvironmentSettingsCollection(SettingSourceFactory.GetSettingSource());
        }

        public Environment(string machineName, string environmentName)
        {
            Init(machineName, environmentName, CreateEnvironmentSettings());
        }

        public Environment(string machineName, string environmentName, ICollection<EnvironmentSetting> settings)
        {
            Init(machineName, environmentName, settings);
        }

        private void Init(string machineName, string environmentName, ICollection<EnvironmentSetting> settings)
        {
            if (string.IsNullOrWhiteSpace(machineName)) throw new ArgumentNullException(machineName);
            if (string.IsNullOrWhiteSpace(environmentName)) throw new ArgumentNullException(environmentName);
            if (settings == null) throw new ArgumentNullException(typeof(EnvironmentSettingsCollection).ToString());

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

        public static Environment Create(string machineName, string environmentName)
        {
            return new Environment(machineName, environmentName);
        }

        public void AddSetting(string name, string provider, string connectionString)
        {
            _settings.Add(EnvironmentSettingFactory.CreateSetting(MachineName, Name, name, provider, connectionString));
        }

        public void AddSetting(string name, string provider)
        {
            AddSetting(name, provider, string.Empty);
        }
    }
}