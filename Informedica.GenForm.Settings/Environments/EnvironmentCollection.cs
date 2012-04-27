using System.Collections;
using System.Collections.Generic;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentCollection: ICollection<Environment>
    {
        private readonly IList<Environment> _environments;

        public EnvironmentCollection(IList<Environment> environments)
        {
            _environments = environments;
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
            return _environments.GetEnumerator();
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
            get { throw new System.NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}
