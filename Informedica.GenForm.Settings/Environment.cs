using System.Collections.Generic;
using Informedica.SecureSettings;

namespace Informedica.GenForm.Settings
{
    public class Environment
    {

        public Environment(string name)
        {
            Init(name, GetLocalMachineName(), CreateEnvironmentSettings());
        }

        private static string GetLocalMachineName()
        {
            return System.Environment.MachineName;
        }

        private static EnvironmentSettings CreateEnvironmentSettings()
        {
            return new EnvironmentSettings(new SettingsManager(new SecureSettingsManager(new SettingSource())));
        }

        public Environment(string name, EnvironmentSettings settings)
        {
            Init(name, GetLocalMachineName(), settings);
        }

        public Environment(string name, string machine)
        {
            Init(name, machine, CreateEnvironmentSettings());
        }

        private void Init(string environmentName, string machineName, EnvironmentSettings settings)
        {
            Name = environmentName;
            MachineName = machineName;
            Settings = settings;
        }

        public static Environment Create(string name)
        {
            return new Environment(name);
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