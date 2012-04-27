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

        public Setting ReadAppSetting(string name)
        {
            if (!ConfigurationContainsAppSetting(name)) throw new SettingNotFoundException(name);
            return CreateSetting(Configuration.AppSettings.Settings[name]);
        }

        private bool ConfigurationContainsAppSetting(string name)
        {
            return Configuration.AppSettings.Settings.AllKeys.Any(k => k == name);
        }

        private static Setting CreateSetting(KeyValueConfigurationElement element)
        {
            return new Setting(element.Key, element.Value, Types.App.ToString(), false);
        }

        protected override void RegisterWriters()
        {
            Writers.Add(Types.App, WriteAppSetting);
            Writers.Add(Types.Conn, WriteConnectionString);
        }

        private void WriteConnectionString(Setting setting)
        {
            if (Configuration.ConnectionStrings.ConnectionStrings[setting.Name] == null) 
                Configuration.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(setting.Name, setting.Value));
            else
            {
                Configuration.ConnectionStrings.ConnectionStrings[setting.Name].ConnectionString = setting.Value;
            }
        }

        private void WriteAppSetting(Setting setting)
        {
            if (Configuration.AppSettings.Settings[setting.Name] == null) 
                Configuration.AppSettings.Settings.Add(new KeyValueConfigurationElement(setting.Name, setting.Value));
            else Configuration.AppSettings.Settings[setting.Name].Value = setting.Value;
        }

        protected override void RegisterRemovers()
        {
            Removers.Add(Types.App, RemoveAppSetting);
            Removers.Add(Types.Conn, RemoveConnectionString);
        }

        private bool RemoveConnectionString(Setting setting)
        {
            if (Configuration.ConnectionStrings.ConnectionStrings[setting.Name] != null)
            {
                Configuration.ConnectionStrings.ConnectionStrings.Remove(setting.Name);
                return true;
            }
            return false;
        }

        private bool RemoveAppSetting(Setting setting)
        {
            if (Configuration.AppSettings.Settings.AllKeys.Any(k => k == setting.Name))
            {
                Configuration.AppSettings.Settings.Remove(setting.Name);
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