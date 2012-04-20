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

        private Setting ReadConnSetting(string arg)
        {
            return Settings.SingleOrDefault(S => S.Name == arg);
        }

        protected override void RegisterWriters()
        {
            Writers.Add(ConfigurationSettingSource.Types.Conn, WriteConnSetting);
        }

        private void WriteConnSetting(Setting obj)
        {
            if (!Settings.Any(s => s.Name == obj.Name)) _settings.Add(obj);
            else Settings.Single(s => s.Name == obj.Name).Value = obj.Value;
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