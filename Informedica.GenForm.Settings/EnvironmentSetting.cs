namespace Informedica.GenForm.Settings
{
    public class EnvironmentSetting
    {

        public EnvironmentSetting(int id, string machineName, string name, string provider, string connectionString)
        {
            Id = id;
            MachineName = machineName;
            Provider = provider;
            Name = name;
            ConnectionString = connectionString;
        }

        public int Id { get; private set; }

        public string MachineName { get; private set; }

        public string Name { get; private set; }

        public string Provider { get; private set; }

        public string SettingName
        {
            get { return MachineName + "." + Name + "." + Provider; }
        }

        public string ConnectionString  { get; set; }

        public bool IsIdentical(EnvironmentSetting setting)
        {
            return Id == setting.Id &&
                   MachineName == setting.MachineName &&
                   Name == setting.Name;
        }
    }
}