using System;
using System.Globalization;
using System.Linq;

namespace Informedica.GenForm.Settings.Environments
{
    public class GenFormEnvironment
    {
        public enum Settings
        {
            Database,
            LogPath,
            ExportPath
        }

        private readonly Environment _environment;

        public GenFormEnvironment(Environment environment)
        {
            _environment = environment;
            CheckEnvironmentSettings();
            CheckIfConnectionStringAndProviderNotEmpty();
        }

        private void CheckEnvironmentSettings()
        {
            if (!CheckIfSettingExists(Settings.Database)) throw new Exception("There is no setting for " + Settings.Database);
            if (!CheckIfSettingExists(Settings.LogPath)) throw new Exception("There is no setting for " + Settings.LogPath);
            if (!CheckIfSettingExists(Settings.ExportPath)) throw new Exception("There is no setting for " + Settings.ExportPath);
        }

        private bool CheckIfSettingExists(Settings settings)
        {
            return (_environment.Settings.SingleOrDefault(s => s.Name == settings.ToString()) != null);
        }

        private void CheckIfConnectionStringAndProviderNotEmpty()
        {
            if (string.IsNullOrWhiteSpace(Database)) throw new Exception("Database connection string cannot be empty");
            if (string.IsNullOrWhiteSpace(Provider)) throw new Exception("Provider cannot be empty");
        }

        public string Name { get { return _environment.Name; } }

        public string Database
        {
            get { return GetDatabaseSetting().ConnectionString; }
            set { GetDatabaseSetting().ConnectionString = value; }
        }

        public string LogPath
        {
            get { return GetLogPathSetting().ConnectionString; }
            set { GetLogPathSetting().ConnectionString = value; }
        }

        public string ExportPath
        {
            get { return GetExportPathSetting().ConnectionString; }
            set { GetExportPathSetting().ConnectionString = value; }
        }

        public string MachineName
        {
            get { return _environment.MachineName; }
        }

        public string Provider
        {
            get { return GetDatabaseSetting().Provider; }
        }

        internal Environment Environmnent
        {
            get { return _environment; }
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Database);
        }

        private EnvironmentSetting GetDatabaseSetting()
        {
            return GetSetting(Settings.Database);
        }

        private EnvironmentSetting GetLogPathSetting()
        {
            return GetSetting(Settings.LogPath);
        }

        private EnvironmentSetting GetExportPathSetting()
        {
            return GetSetting(Settings.ExportPath);
        }

        private EnvironmentSetting GetSetting(Enum setting)
        {
            return _environment.Settings.Single(s => s.Name == setting.ToString(CultureInfo.InvariantCulture));
        }

    }
}