using System.Configuration;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public static class EnvironmentSettingFactory
    {

        public static EnvironmentSetting CreateSetting(string machine, string environment, string name, string provider, string value)
        {
            var setting =
                SettingFactory.CreateSecureSetting<ConnectionStringSettings>(
                    GetSettingKey(machine, environment, name, provider), value);

            return new EnvironmentSetting(setting);
        }

        private static string GetSettingKey(string machine, string environment, string name, string provider)
        {
            return machine + EnvironmentSetting.Seperator + environment + EnvironmentSetting.Seperator + name + EnvironmentSetting.Seperator + provider;
        }

        public static EnvironmentSetting CreateSetting(ISetting setting)
        {
            return new EnvironmentSetting(setting);
        }
    }
}