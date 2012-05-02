using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            foreach (var setting in env.Settings)
            {
                _settings.Add(setting);
            }
        }

        private bool RemoveEnvironment(Environment env)
        {
            return Environments.Remove(env);
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
            return Environments.GetEnumerator();
        }

        private IList<Environment> Environments
        {
            get
            {
                RefreshEnvironments();
                return _environments;
            }
        }

        private void RefreshEnvironments()
        {
            _environments = new List<Environment>();
            foreach (var setting in _settings)
            {
                var env = new Environment(setting.MachineName, setting.Environment, _settings);
                if (!_environments.Any(e => e.MachineName == env.MachineName &&
                                            e.Name == env.Name)) _environments.Add(env);
            }
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
                return Environments.Count;
            }
        }

        public bool IsReadOnly
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}
