using System;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSetting
    {
        private SettingsManager _manager;
        private Setting _setting;
        private SecureSettingSource _source;
        public const string Seperator = ".";

        [Obsolete]
        public EnvironmentSetting(string name, string machineName, string environment, string provider, string connectionString, SettingsManager manager)
        {
            if (manager == null) throw new NullReferenceException("Settingsmanager cannot be null");
            _manager = manager;

            Init(name, machineName, environment, provider, connectionString);
        }

        private void Init(string name, string machineName, string environment, string provider, string connectionString)
        {
            Name = name;
            MachineName = machineName;
            Environment = environment;
            Provider = provider;
            //ConnectionString_New = connectionString;
        }

        public string ConnectionString_New
        {
            get { return _source.ReadSecure(ConfigurationSettingSource.Types.Conn, SettingName).Value; } 
            set { _source.WriteSecure(new Setting(SettingName, value, "Conn", true)); }
        }

        public EnvironmentSetting(string name, string machineName, string environment, string provider, string connectionString, SecureSettingSource secureSettingSource)
        {
            _source = secureSettingSource;
            Init(name, machineName, environment, provider, connectionString);
        }

        public string Name { get; private set; }

        public string MachineName { get; private set; }

        public string Environment { get; private set; }

        public string Provider { get; set; }

        [Obsolete]
        public string ConnectionString { get { return _manager.GetConnectionString(SettingName).ConnectionString; } set { _manager.AddConnectionString(SettingName, value); } }

        public string SettingName
        {
            get { return Name + Seperator + MachineName + Seperator + Environment; }
        }

        public bool IsIdentical(EnvironmentSetting setting)
        {
            return setting.SettingName == SettingName;
        }

        [Obsolete]
        public static EnvironmentSetting CreateEnvironmentSetting(string name, string machinename, string environment, SettingsManager manager)
        {
            return new EnvironmentSetting(name, machinename, environment, string.Empty, string.Empty, manager);
        }

        public static EnvironmentSetting CreateEnvironmentSetting(string name, string machine, string environment, SecureSettingSource source)
        {
            return new EnvironmentSetting(name, machine, environment, string.Empty, string.Empty, source);
        }
    }
}