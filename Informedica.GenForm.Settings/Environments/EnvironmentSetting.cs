using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSetting
    {
        private SecureSettingSource _source;
        public const string Seperator = ".";

        private void Init(string name, string machineName, string environment, string provider)
        {
            Name = name;
            MachineName = machineName;
            Environment = environment;
            Provider = provider;
        }

        public string ConnectionString
        {
            get { return _source.ReadSecure(ConfigurationSettingSource.Types.Conn, SettingName).Value; } 
            set { _source.WriteSecure(new Setting(SettingName, value, "Conn", true)); }
        }

        public EnvironmentSetting(string machineName, string environment, string name, string provider, SecureSettingSource secureSettingSource)
        {
            _source = secureSettingSource;
            Init(name, machineName, environment, provider);
        }

        public string Name { get; private set; }

        public string MachineName { get; private set; }

        public string Environment { get; private set; }

        public string Provider { get; private set; }

        public string SettingName
        {
            get { return MachineName + Seperator + Environment + Seperator + Name + Seperator + Provider; }
        }

        public bool IsIdentical(EnvironmentSetting setting)
        {
            return setting.SettingName == SettingName;
        }

        public static EnvironmentSetting CreateEnvironmentSetting(string name, string machine, string environment, string provider, SecureSettingSource source)
        {
            return new EnvironmentSetting(machine, environment, name, provider, source);
        }

    }
}