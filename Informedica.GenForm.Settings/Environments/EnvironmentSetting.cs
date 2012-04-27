using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSetting
    {
        private ICollection<Setting> _source;
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
            get { return ReadConnectionStringSetting(); } 
            set { _source.Add(new Setting(SettingName, value, "Conn", true)); }
        }

        private string ReadConnectionStringSetting()
        {
            var setting =
                _source.SingleOrDefault(
                    s => s.Type == ConfigurationSettingSource.Types.Conn.ToString() && s.Name == Name);

            return setting == null ? string.Empty : setting.Value;
        }

        public EnvironmentSetting(string machineName, string environment, string name, string provider, ICollection<Setting> secureSettingSource)
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

        public static EnvironmentSetting CreateEnvironmentSetting(string name, string machine, string environment, string provider, ICollection<Setting> source)
        {
            return new EnvironmentSetting(machine, environment, name, provider, source);
        }

    }
}