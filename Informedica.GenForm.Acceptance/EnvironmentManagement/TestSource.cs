using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class TestSource: SettingSource
    {
        private IList<Setting> _settings;

        #region Overrides of SettingSource

        protected override Enum SettingTypeToEnum(Setting setting)
        {
            return ConfigurationSettingSource.Types.Conn;
        }

        protected override void RegisterWriters()
        {
            Writers.Add(ConfigurationSettingSource.Types.Conn, WriteConnSetting);
        }

        private void WriteConnSetting(Setting setting)
        {
            _settings.Add(setting);
        }

        protected override void RegisterRemovers()
        {
            Removers.Add(ConfigurationSettingSource.Types.Conn, RemoveConnSetting);
        }

        private bool RemoveConnSetting(Setting setting)
        {
            if (Settings.Any(s => s.Key == setting.Key))
            {
                _settings.Remove(Settings.Single(s => s.Key == setting.Key));
                return true;
            }
            return false;
        }

        protected override IEnumerable<Setting> Settings
        {
            get { return _settings ?? (_settings =  new List<Setting>()); }
        }

        public override void Save()
        {
        }

        new public void Clear()
        {
            _settings.Clear();
        }

        #endregion
    }
}