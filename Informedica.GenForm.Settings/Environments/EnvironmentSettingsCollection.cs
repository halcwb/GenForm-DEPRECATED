using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSettingsCollection : IEnumerable<EnvironmentSetting>
    {
        public const char Separator = '.';

        private readonly string _machine;
        private readonly string _environment;
        private ICollection<Setting> _source;

        public EnvironmentSettingsCollection(string machine, string environment, ICollection<Setting> source)
        {
            _machine = machine;
            _environment = environment;
            _source = source;

            CheckConstruction();
        }

        private void CheckConstruction()
        {
            if (string.IsNullOrWhiteSpace(_machine)) throw new Exception("Machine name not supplied");
            if (string.IsNullOrWhiteSpace(_environment)) throw new Exception("Environment name not supplied");
            if (_source == null) throw new Exception("Source not supplied");
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<EnvironmentSetting> GetEnumerator()
        {
            return GetEnvironmentSettings().GetEnumerator();
        }

        private IEnumerable<EnvironmentSetting> GetEnvironmentSettings()
        {
            var source = new List<Setting>(_source);
            var result = (from setting in source
                          where setting.Type == ConfigurationSettingSource.Types.Conn.ToString()
                          where CheckIfNameIsAValidSettingName(setting.Key)
                          select new EnvironmentSetting(setting.Machine,
                                                        setting.Environment,
                                                        GetNameFromSettingName(setting.Key),
                                                        GetProviderFromSettingName(setting.Key),
                                                        _source) { ConnectionString = setting.Value }).ToList();
            return result;
        }

        private static bool CheckIfNameIsAValidSettingName(string name)
        {
            return name.Split(Separator).GetUpperBound(0) >= 3;
        }

        private string GetNameFromSettingName(string settingName)
        {
            return settingName.Split(Separator)[2];
        }

        private string GetProviderFromSettingName(string settingName)
        {
            return settingName.Split(Separator)[3];
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

        public void AddSetting(string name, string provider)
        {
            AddSetting(name, provider, string.Empty);
        }

        private void AddSetting(EnvironmentSetting environmentSetting, string value)
        {
            _source.Add(new Setting(environmentSetting.SettingName, value, "Conn", true));
        }

        public void RemoveEnvironmentSetting(string name, string provider)
        {
            var setting = EnvironmentSetting.CreateEnvironmentSetting(name, _machine, _environment, provider, _source);
            _source.Remove(_source.SingleOrDefault(s => s.Key == setting.SettingName));
        }

        public void AddSetting(string name, string provider, string value)
        {
            if (this.Any(envset => envset.Name == name)) throw new DuplicateSettingError("EnvironmentSetting with name " + name + "already exists");

            var setting = EnvironmentSetting.CreateEnvironmentSetting(name, _machine, _environment, provider, _source);
            AddSetting(setting, value);
        }
    }
}