using System;

namespace Informedica.GenForm.Settings
{
    public class EnvironmentSetting
    {
        private SettingsManager _manager;
        public const string Seperator = ".";

        public EnvironmentSetting(string name, string machineName, string environment, string provider, string connectionString, SettingsManager manager)
        {
            if (manager == null) throw new NullReferenceException("Settingsmanager cannot be null");
            _manager = manager;

            Name = name;
            MachineName = machineName;
            Environment = environment;
            Provider = provider;
            ConnectionString = connectionString;    
        }

        public string Name { get; private set; }

        public string MachineName { get; private set; }

        public string Environment { get; private set; }

        public string Provider { get; set; }

        public string ConnectionString { get { return _manager.GetConnectionString(SettingName).ConnectionString; } set { _manager.AddConnectionString(SettingName, value); } }

        public string SettingName
        {
            get { return Name + Seperator + MachineName + Seperator + Environment; }
        }

        public bool IsIdentical(EnvironmentSetting setting)
        {
            return setting.SettingName == SettingName;
        }

        public static EnvironmentSetting CreateEnvironment(string name, string machinename, string environment, SettingsManager manager)
        {
            return new EnvironmentSetting(name, machinename, environment, string.Empty, string.Empty, manager);
        }
    }
}