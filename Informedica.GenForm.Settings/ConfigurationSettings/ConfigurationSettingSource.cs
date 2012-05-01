using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.ConfigurationSettings
{
    public class ConfigurationSettingSource : SettingSource
    {
        private Configuration _configuration;
        private readonly ConfigurationFactory _factory;

        #region Overrides of SettingSource

        public ConfigurationSettingSource(ConfigurationFactory factory)
        {
            _factory = factory;
        }

        protected override void RegisterWriters()
        {
            Writers.Add(typeof(KeyValueConfigurationElement), WriteAppSetting);
            Writers.Add(typeof(ConnectionStringSettings), WriteConnectionString);
        }

        private void WriteConnectionString(ISetting setting)
        {
            if (Configuration.ConnectionStrings.ConnectionStrings[setting.Key] == null) 
                Configuration.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(setting.Key, setting.Value));
            else
            {
                Configuration.ConnectionStrings.ConnectionStrings[setting.Key].ConnectionString = setting.Value;
            }
        }

        private void WriteAppSetting(ISetting setting)
        {
            if (Configuration.AppSettings.Settings[setting.Key] == null) 
                Configuration.AppSettings.Settings.Add(new KeyValueConfigurationElement(setting.Key, setting.Value));
            else Configuration.AppSettings.Settings[setting.Key].Value = setting.Value;
        }

        protected override void RegisterRemovers()
        {
            Removers.Add(typeof(KeyValueConfigurationElement), RemoveAppSetting);
            Removers.Add(typeof(ConnectionStringSettings), RemoveConnectionString);
        }

        private bool RemoveConnectionString(ISetting setting)
        {
            if (Configuration.ConnectionStrings.ConnectionStrings[setting.Key] != null)
            {
                Configuration.ConnectionStrings.ConnectionStrings.Remove(setting.Key);
                return true;
            }
            return false;
        }

        private bool RemoveAppSetting(ISetting setting)
        {
            if (Configuration.AppSettings.Settings.AllKeys.Any(k => k == setting.Key))
            {
                Configuration.AppSettings.Settings.Remove(setting.Key);
                return true;
            }
            return false;
        }

        protected override IEnumerable<ISetting> Settings
        {
            get
            {
                var list = new List<ISetting>();
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

        private IEnumerable<ISetting> ReadConnectionStrings()
        {
            return (from ConnectionStringSettings s in Configuration.ConnectionStrings.ConnectionStrings
                    select SettingFactory.CreateSecureSetting(s)).ToList();
        }

        private IEnumerable<ISetting> ReadAppSettings()
        {
            return (from KeyValueConfigurationElement e in Configuration.AppSettings.Settings 
                    select SettingFactory.CreateSecureSetting(e)).ToList();
        }

        #endregion

        public Configuration Configuration
        {
            get { return _configuration ?? (_configuration = _factory.GetConfiguration()); }
        }

    }
}