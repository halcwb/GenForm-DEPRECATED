using System.Collections;
using System.Collections.Generic;

namespace Informedica.GenForm.Settings
{
    public class Environments: IEnumerable<Environment>
    {
        private readonly IList<Environment> _environments;

        public Environments(IList<Environment> environments)
        {
            _environments = environments;
        }

        public void AddEnvironment(Environment env)
        {
            _environments.Add(env);
        }

        public void RemoveEnvironment(Environment env)
        {
            _environments.Remove(env);
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
    }
}
