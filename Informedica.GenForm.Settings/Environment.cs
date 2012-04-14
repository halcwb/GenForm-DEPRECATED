using Informedica.SecureSettings;

namespace Informedica.GenForm.Settings
{
    public class Environment
    {
        private static EnvironmentSettings CreateEnvironmentSettings(string machine, string environment)
        {
            return new EnvironmentSettings(new SettingsManager(new SecureSettingSource(new WebConfigSettingSource())), machine, environment);
        }

        public Environment(string machine, string name, EnvironmentSettings settings)
        {
            Init(name, machine, settings);
        }

        public Environment(string name, string machine)
        {
            Init(name, machine, CreateEnvironmentSettings(machine, name));
        }

        private void Init(string environmentName, string machineName, EnvironmentSettings settings)
        {
            Name = environmentName;
            MachineName = machineName;
            Settings = settings;
        }

        public string Name { get; private set; }

        public EnvironmentSettings Settings { get; private set; }

        public string MachineName { get; private set; }

        public static Environment Create(string name, string machine)
        {
            return new Environment(name, machine);
        }
    }
}