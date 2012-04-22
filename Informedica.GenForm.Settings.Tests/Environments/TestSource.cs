using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    public class TestSource: SettingSource
    {
        private IList<Setting> _settings;

        #region Overrides of SettingSource

        protected override Enum SettingTypeToEnum(Setting setting)
        {
            return ConfigurationSettingSource.Types.Conn;
        }

        protected override void RegisterReaders()
        {
            Readers.Add(ConfigurationSettingSource.Types.Conn, ReadConnSetting);
        }

        private Setting ReadConnSetting(string name)
        {
            return Settings.SingleOrDefault(S => S.Name == name);
        }

        protected override void RegisterWriters()
        {
            Writers.Add(ConfigurationSettingSource.Types.Conn, WriteConnSetting);
        }

        private void WriteConnSetting(Setting setting)
        {
            var old = (Settings.SingleOrDefault(s => s.Name == setting.Name));
            if (old != null) _settings.Remove(old);
            _settings.Add(setting);
        }

        protected override void RegisterRemovers()
        {
            Removers.Add(ConfigurationSettingSource.Types.Conn, RemoveConnSetting);
        }

        private void RemoveConnSetting(Setting setting)
        {
            if (Settings.Any(s => s.Name == setting.Name)) _settings.Remove(Settings.Single(s => s.Name == setting.Name));
        }

        protected override IEnumerable<Setting> Settings
        {
            get { return _settings ?? (_settings =  new List<Setting>()); }
        }

        public override void Save()
        {
        }

        #endregion
    }
}