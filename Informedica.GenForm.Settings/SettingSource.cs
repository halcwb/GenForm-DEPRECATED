using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using Informedica.SecureSettings;

namespace Informedica.GenForm.Settings
{
    public class SettingSource: ISettingSource
    {
        private static readonly IList<Setting> Settings = new List<Setting>();
        private static Configuration _configuration;

        private readonly string _virtualPath = "/GenForm";

        public SettingSource() {}

        public SettingSource(string path)
        {
            _virtualPath = path;
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<Setting> GetEnumerator()
        {
            Settings.Clear();
            ReloadSettings();

            return Settings.GetEnumerator();
        }

        private void ReloadSettings()
        {
            foreach (ConnectionStringSettings connstr in Configuration.ConnectionStrings.ConnectionStrings)
            {
                Settings.Add(GetConnectionSetting(connstr));
            }
            foreach (KeyValueConfigurationElement appSetting in Configuration.AppSettings.Settings)
            {
                Settings.Add(GetAppSetting(appSetting));
            }
        }

        private static Setting GetAppSetting(KeyValueConfigurationElement setting)
        {
            return new Setting(setting.Key, setting.Value, "app", false);
        }

        private static Setting GetConnectionSetting(ConnectionStringSettings connstr)
        {
            return new Setting(connstr.Name, connstr.ConnectionString, "conn", SettingIsEncrypted(connstr.Name));
        }

        private static bool SettingIsEncrypted(string name)
        {
            return name.StartsWith(SecureSettingsManager.SecureMarker);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ISettingSource

        public void WriteConnectionString(string name, string connectionString)
        {
            var conset = Configuration.ConnectionStrings.ConnectionStrings[name];
            if (conset == null)
            {
                conset = new ConnectionStringSettings(name, connectionString);
                Configuration.ConnectionStrings.ConnectionStrings.Add(conset);
            }
            else
            {
                Configuration.ConnectionStrings.ConnectionStrings[name].ConnectionString = connectionString;
            }

            SaveConfiguration();
        }

        public string ReadConnectionString(string name)
        {
            return Configuration.ConnectionStrings.ConnectionStrings[name] == null ?
                string.Empty:
                Configuration.ConnectionStrings.ConnectionStrings[name].ConnectionString;
        }

        public void WriteAppSetting(string name, string value)
        {
            if (Configuration.AppSettings.Settings[name] == null)
            {
                var setting  = new KeyValueConfigurationElement(name, value);
                Configuration.AppSettings.Settings.Add(setting);
            }

            Configuration.AppSettings.Settings[name].Value = value;
            SaveConfiguration();
        }

        private Configuration Configuration
        {
            get { return _configuration ?? (_configuration = WebConfigurationManager.OpenWebConfiguration(_virtualPath)); }
        }

        private void SaveConfiguration()
        {
            Configuration.Save();
            _configuration = null;
        }

        public string ReadAppSetting(string name)
        {
            return Configuration.AppSettings.Settings[name] == null ? 
                String.Empty:
                Configuration.AppSettings.Settings[name].Value;
        }

        public void Remove(string setting)
        {
            Configuration.AppSettings.Settings.Remove(setting);
            SaveConfiguration();
        }

        public void Remove(Setting setting)
        {
            Remove(setting.Name);
        }

        public void RemoveConnectionString(string name)
        {
            var setting = Configuration.ConnectionStrings.ConnectionStrings[name] ??
                          Configuration.ConnectionStrings.ConnectionStrings[SecureSettingsManager.SecureMarker + name];

            Configuration.ConnectionStrings.ConnectionStrings.Remove(setting);
            SaveConfiguration();
        }

        #endregion
    }
}