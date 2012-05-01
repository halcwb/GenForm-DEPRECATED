using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    public class TestSource: SettingSource
    {
        private IList<ISetting> _settings;

        #region Overrides of SettingSource


        protected override void RegisterWriters()
        {
            Writers.Add(typeof(ConnectionStringSettings), WriteConnSetting);
        }

        private void WriteConnSetting(ISetting setting)
        {
            var old = (Settings.SingleOrDefault(s => s.Key == setting.Key));
            if (old != null) _settings.Remove(old);
            _settings.Add(setting);
        }

        protected override void RegisterRemovers()
        {
            Removers.Add(typeof(ConnectionStringSettings), RemoveConnSetting);
        }

        private bool RemoveConnSetting(ISetting setting)
        {
            if (Settings.Any(s => s.Key == setting.Key))
            {
                _settings.Remove(Settings.Single(s => s.Key == setting.Key));
                return true;
            }
            return false;
        }

        protected override IEnumerable<ISetting> Settings
        {
            get { return _settings ?? (_settings =  new List<ISetting>()); }
        }

        public override void Save()
        {
        }

        #endregion
    }
}