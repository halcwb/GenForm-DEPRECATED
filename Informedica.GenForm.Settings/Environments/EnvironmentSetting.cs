using System;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSetting
    {
        private SecureSettingSource _source;
        public const string Seperator = ".";

        private void Init(string name, string machineName, string environment)
        {
            Name = name;
            MachineName = machineName;
            Environment = environment;
        }

        public string ConnectionString
        {
            get { return _source.ReadSecure(ConfigurationSettingSource.Types.Conn, SettingName).Value; } 
            set { _source.WriteSecure(new Setting(SettingName, value, "Conn", true)); }
        }

        public EnvironmentSetting(string name, string machineName, string environment, SecureSettingSource secureSettingSource)
        {
            _source = secureSettingSource;
            Init(name, machineName, environment);
        }

        public string Name { get; private set; }

        public string MachineName { get; private set; }

        public string Environment { get; private set; }

        public string Provider { get; set; }

        public string SettingName
        {
            get { return Name + Seperator + MachineName + Seperator + Environment; }
        }

        public bool IsIdentical(EnvironmentSetting setting)
        {
            return setting.SettingName == SettingName;
        }

        public static EnvironmentSetting CreateEnvironmentSetting(string name, string machine, string environment, SecureSettingSource source)
        {
            return new EnvironmentSetting(name, machine, environment, source);
        }

    }
}