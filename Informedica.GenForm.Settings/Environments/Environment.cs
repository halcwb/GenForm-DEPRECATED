using Informedica.GenForm.Settings.ConfigurationSettings;

namespace Informedica.GenForm.Settings.Environments
{
    public class Environment
    {
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
            Settings = settings;
        }

        public string Name { get; private set; }

        public EnvironmentSettingsCollection Settings { get; private set; }

        public string MachineName { get; private set; }

        public static Environment Create(string name, string machine)
        {
            return new Environment(name, machine);
        }
    }
}