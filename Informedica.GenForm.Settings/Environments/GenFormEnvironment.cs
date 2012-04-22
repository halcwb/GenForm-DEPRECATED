using System;
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
            CheckIfDatabaseConnectionStringIsNotNullOrWhiteSpace();
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

        private void CheckIfDatabaseConnectionStringIsNotNullOrWhiteSpace()
        {
            if (string.IsNullOrWhiteSpace(Database)) throw new Exception("Database connection string cannot be empty");
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

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Database);
        }

        private EnvironmentSetting GetDatabaseSetting()
        {
            return GetSetting("Database");
        }

        private EnvironmentSetting GetLogPathSetting()
        {
            return GetSetting("ExportPath");
        }

        private EnvironmentSetting GetExportPathSetting()
        {
            return GetSetting("LogPath");
        }

        private EnvironmentSetting GetSetting(string name)
        {
            return _environment.Settings.Single(s => s.Name == name);
        }

    }
}