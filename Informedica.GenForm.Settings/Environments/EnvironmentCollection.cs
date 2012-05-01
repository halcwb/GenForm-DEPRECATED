using System.Collections;
using System.Collections.Generic;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentCollection: ICollection<Environment>
    {
        private IList<Environment> _environments;
        private ICollection<EnvironmentSetting> _settings;

        public EnvironmentCollection(ICollection<EnvironmentSetting> settings)
        {
            _settings = settings;
        }

        public EnvironmentCollection(List<Environment> environments)
        {
            throw new System.NotImplementedException();
        }

        private void AddEnvironment(Environment env)
        {
            _environments.Add(env);
        }

        private bool RemoveEnvironment(Environment env)
        {
            return _environments.Remove(env);
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<Environment> GetEnumerator()
        {
            RefreshEnvironments();

            return _environments.GetEnumerator();
        }

        private void RefreshEnvironments()
        {
            _environments = new List<Environment>();
            foreach (var setting in _settings)
            {
                var env = new Environment(setting.MachineName, setting.Environment, _settings);
                _environments.Add(env);
            }
        }

        //private void RefreshEnvironments()
        //{
        //    _environments = new List<Environment>();
        //    var source = new List<ISetting>(_source);
        //    source.Sort(KeyOrder);

        //    var machine = string.Empty;
        //    var environment = string.Empty;

        //    foreach (var setting in source)
        //    {
        //        if (setting.Machine == machine && setting.Environment == environment) continue;

        //        machine = setting.Machine;
        //        environment = setting.Environment;

        //        if (string.IsNullOrWhiteSpace(machine) || string.IsNullOrWhiteSpace(environment)) continue;

        //        _environments.Add(new Environment(machine, environment,
        //                                          new EnvironmentSettingsCollection(machine, environment, _source)));
        //    }
        //}


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

        public IEnumerable<Environment> GetEnvironmentsForMachine(string mymachine)
        {
            throw new System.NotImplementedException();
        }

        #region Implementation of ICollection<Environment>

        public void Add(Environment item)
        {
            AddEnvironment(item);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(Environment item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(Environment[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Environment item)
        {
            return RemoveEnvironment(item);
        }

        public int Count
        {
            get
            {   
                RefreshEnvironments();
                return _environments.Count;
            }
        }

        public bool IsReadOnly
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}
