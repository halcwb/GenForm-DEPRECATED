using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.ConfigurationSettings
{
    public class ConfigurationSettingSource : SettingSource
    {
        public enum Types
        {
            App,
            Conn
        }

        private Configuration _configuration;
        private readonly ConfigurationFactory _factory;

        #region Overrides of SettingSource

        public ConfigurationSettingSource(ConfigurationFactory factory)
        {
            _factory = factory;
        }

        protected override Enum SettingTypeToEnum(Setting setting)
        {
            if (setting.Type == Types.App.ToString()) return Types.App;
            if (setting.Type == Types.Conn.ToString()) return Types.Conn;
            throw new UnknownSettingTypeException();
        }

        protected override void RegisterWriters()
        {
            Writers.Add(Types.App, WriteAppSetting);
            Writers.Add(Types.Conn, WriteConnectionString);
        }

        private void WriteConnectionString(Setting setting)
        {
            if (Configuration.ConnectionStrings.ConnectionStrings[setting.Key] == null) 
                Configuration.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(setting.Key, setting.Value));
            else
            {
                Configuration.ConnectionStrings.ConnectionStrings[setting.Key].ConnectionString = setting.Value;
            }
        }

        private void WriteAppSetting(Setting setting)
        {
            if (Configuration.AppSettings.Settings[setting.Key] == null) 
                Configuration.AppSettings.Settings.Add(new KeyValueConfigurationElement(setting.Key, setting.Value));
            else Configuration.AppSettings.Settings[setting.Key].Value = setting.Value;
        }

        protected override void RegisterRemovers()
        {
            Removers.Add(Types.App, RemoveAppSetting);
            Removers.Add(Types.Conn, RemoveConnectionString);
        }

        private bool RemoveConnectionString(Setting setting)
        {
            if (Configuration.ConnectionStrings.ConnectionStrings[setting.Key] != null)
            {
                Configuration.ConnectionStrings.ConnectionStrings.Remove(setting.Key);
                return true;
            }
            return false;
        }

        private bool RemoveAppSetting(Setting setting)
        {
            if (Configuration.AppSettings.Settings.AllKeys.Any(k => k == setting.Key))
            {
                Configuration.AppSettings.Settings.Remove(setting.Key);
                return true;
            }
            return false;
        }

        protected override IEnumerable<Setting> Settings
        {
            get
            {
                var list = new List<Setting>();
                list.AddRange(ReadAppSettings());
                list.AddRange(ReadConnectionStrings());
                return list;
            }
        }

        public override void Save()
        {
            Configuration.Save();
            // Have to reload configuration, otherwise an 'out of sync' error is thrown
            _configuration = null;
        }

        private IEnumerable<Setting> ReadConnectionStrings()
        {
            return (from ConnectionStringSettings connstring in Configuration.ConnectionStrings.ConnectionStrings
                    select new Setting(connstring.Name, connstring.ConnectionString, Types.Conn.ToString(), false)).ToList();
        }

        private IEnumerable<Setting> ReadAppSettings()
        {
            return (from KeyValueConfigurationElement element in Configuration.AppSettings.Settings 
                    select new Setting(element.Key, element.Value, Types.App.ToString(), false)).ToList();
        }

        #endregion

        public Configuration Configuration
        {
            get { return _configuration ?? (_configuration = _factory.GetConfiguration()); }
        }

    }
}