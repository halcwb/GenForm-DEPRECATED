using System;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSetting
    {
        private const int MachineNameIndex = 0;
        private const int EnvironmentIndex = 1;
        private const int NameIndex = 2;
        private const int ProviderIndex = 3;

        public const char Seperator = '.';

        public string ConnectionString
        {
            get { return ReadConnectionStringSetting(); } 
            set { WriteConnectionString(value); }
        }

        private void WriteConnectionString(string value)
        {
            Setting.Value = value;
        }

        private ISetting GetConnectionString()
        {
            return Setting;
        }

        private string ReadConnectionStringSetting()
        {
            return GetConnectionString() == null ? String.Empty : GetConnectionString().Value;
        }


        public EnvironmentSetting(string machineName, string environment, string name, string provider, ISetting setting)
        {
            Setting = setting;
            MachineName = machineName;
            Environment = environment;
            Name = name;
            Provider = provider;
        }

        public EnvironmentSetting(ISetting setting)
        {
            Setting = setting;
            Name = GetNameFromSettingName(setting);
            MachineName = GetMachineName(setting);
            Environment = GetEnvironment(setting);
            Provider = GetProviderFromSettingName(setting);
        }

        public string Name { get; private set; }

        public string MachineName { get; private set; }

        public string Environment { get; private set; }

        public string Provider { get; private set; }

        public string SettingName
        {
            get { return MachineName + Seperator + Environment + Seperator + Name + Seperator + Provider; }
        }

        internal ISetting Setting { get; private set; }

        public bool IsIdentical(EnvironmentSetting setting)
        {
            return setting.SettingName == SettingName;
        }

        public static bool CheckIfNameIsAValidSettingName(string name)
        {
            return name.Split(Seperator).GetUpperBound(0) >= ProviderIndex;
        }

        public static string GetMachineName(ISetting setting)
        {
            return GetPartOfSettingKey(setting, MachineNameIndex);
        }

        public static string GetEnvironment(ISetting setting)
        {
            return GetPartOfSettingKey(setting, EnvironmentIndex);
        }

        public static string GetNameFromSettingName(ISetting setting)
        {
            return GetPartOfSettingKey(setting, NameIndex);
        }

        public static string GetProviderFromSettingName(ISetting setting)
        {
            return GetPartOfSettingKey(setting, ProviderIndex);
        }

        private static string GetPartOfSettingKey(ISetting setting, int index)
        {
            return setting.Key.Split(Seperator)[index];
        }
    }
}