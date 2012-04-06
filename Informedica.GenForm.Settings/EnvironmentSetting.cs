namespace Informedica.GenForm.Settings
{
    public class EnvironmentSetting
    {

        public EnvironmentSetting(string machineName, string name, string provider, string connectionString)
        {
            MachineName = machineName;
            Provider = provider;
            Name = name;
            ConnectionString = connectionString;
        }

        public string MachineName { get; private set; }

        public string Name { get; private set; }

        public string Provider { get; private set; }

        public string SettingName
        {
            get { return MachineName + "." + Name + "." + Provider; }
        }

        public string ConnectionString  { get; set; }
    }
}