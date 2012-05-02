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
        private ICollection<ISetting> _source;
        private IList<EnvironmentSetting> _settings = new List<EnvironmentSetting>();

        public EnvironmentSettingsCollection(ICollection<ISetting> source)
        {
            _source = source;
            CheckConstruction();

            RefreshCollection();
        }

        private void CheckConstruction()
        {
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
            return Settings.GetEnumerator();
        }

        private IEnumerable<EnvironmentSetting> Settings
        {
            get
            {
                return _settings;
            }
        }

        private void RefreshCollection()
        {
            _settings = new List<EnvironmentSetting>();

            foreach (var setting in _source)
            {
                if (EnvironmentSetting.CheckIfNameIsAValidSettingName(setting.Key) &&
                    setting.Type == typeof (ConnectionStringSettings))
                {
                    var envSet = EnvironmentSettingFactory.CreateSetting(setting);
                    if (!ContainsEnvironmentSetting(envSet)) _settings.Add(envSet);
                }
            }
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
            
            RefreshCollection();
        }

        public void RemoveEnvironmentSetting(string name, string provider)
        {
            var setting = GetSettingFromSource(this.Single(s => s.Name == name && s.Provider == provider));
            _source.Remove(_source.SingleOrDefault(s => s.Key == setting.Key));
            
            RefreshCollection();
        }

        private ISetting GetSettingFromSource(EnvironmentSetting setting)
        {
            return _source.SingleOrDefault(s => s.Key == setting.SettingName);
        }

        public void AddSetting(string machine, string environment, string name, string provider, string value)
        {
            var envSet = EnvironmentSettingFactory.CreateSetting(machine, environment, name, provider, value);
            if (ContainsEnvironmentSetting(envSet)) throw new DuplicateSettingError("EnvironmentSetting with name " + name + "already exists");

            AddSetting(envSet);
        }

        #region Implementation of ICollection<EnvironmentSetting>

        public void Add(EnvironmentSetting item)
        {
            AddSetting(item);
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
            get { return Settings.Count(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public void AddSetting(string machine, string environment, string name, string provider)
        {
            AddSetting(machine, environment, name, provider, string.Empty);
        }
    }
}