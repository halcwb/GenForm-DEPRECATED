namespace Informedica.GenForm.Settings
{
    public class EnvironmentSetting
    {

        public EnvironmentSetting(string name, string machineName, string environment, string provider, string connectionString)
        {
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

        public string SettingName
        {
            get { return MachineName + "." + Environment + "." + Name; }
        }

        public string ConnectionString  { get; set; }

        public bool IsIdentical(EnvironmentSetting setting)
        {
            return Name == setting.Name &&
                   MachineName == setting.MachineName &&
                   Environment == setting.Environment;
        }

        public static EnvironmentSetting CreateEnvironment(string name, string machinename, string environment)
        {
            return new EnvironmentSetting(name, machinename, environment, string.Empty, string.Empty);
        }
    }
}