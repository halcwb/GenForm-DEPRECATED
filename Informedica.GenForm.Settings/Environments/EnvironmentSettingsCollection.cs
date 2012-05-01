using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSettingsCollection : ICollection<EnvironmentSetting>
    {
        private readonly string _machine;
        private readonly string _environment;
        private ICollection<ISetting> _source;
        private IList<EnvironmentSetting> _settings;

        public EnvironmentSettingsCollection(string machine, string environment, ICollection<ISetting> source)
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
            _settings = new List<EnvironmentSetting>();

            foreach (var setting in _source)
            {
                if (EnvironmentSetting.CheckIfNameIsAValidSettingName(setting.Key) && setting.Type == typeof(ConnectionStringSettings))
                {
                    var envSet = EnvironmentSettingFactory.CreateSetting(setting);
                    if (envSet.MachineName == _machine && envSet.Environment == _environment && !ContainsEnvironmentSetting(envSet)) _settings.Add(envSet);
                }
            }

            return _settings;
        }

        private bool ContainsEnvironmentSetting(EnvironmentSetting envSet)
        {
            return
                _settings.Any(
                    s =>
                    s.MachineName == envSet.MachineName && s.Environment == envSet.Environment &&
                    s.Name == envSet.Name);
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

        private void AddSetting(EnvironmentSetting environmentSetting)
        {
            _source.Add(environmentSetting.Setting);
        }

        public void RemoveEnvironmentSetting(string name, string provider)
        {
            var setting = GetSettingFromSource(this.Single(s => s.Name == name && s.Provider == provider));
            _source.Remove(_source.SingleOrDefault(s => s.Key == setting.Key));
        }

        private ISetting GetSettingFromSource(EnvironmentSetting setting)
        {
            return _source.SingleOrDefault(s => s.Key == setting.SettingName);
        }

        public void AddSetting(string name, string provider)
        {
            AddSetting(name, provider, string.Empty);
        }

        public void AddSetting(string name, string provider, string value)
        {
            if (this.Any(envset => envset.Name == name)) throw new DuplicateSettingError("EnvironmentSetting with name " + name + "already exists");

            var setting = EnvironmentSettingFactory.CreateSetting(_machine, _environment, name, provider, value);
            AddSetting(setting);
        }

        #region Implementation of ICollection<EnvironmentSetting>

        public void Add(EnvironmentSetting item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(EnvironmentSetting item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(EnvironmentSetting[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(EnvironmentSetting item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return GetEnvironmentSettings().Count(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}