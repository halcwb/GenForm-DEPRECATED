using System.Collections;
using System.Collections.Generic;

namespace Informedica.GenForm.Settings.Environments
{
    public class GenFormEnvironmentCollection: IEnumerable<GenFormEnvironment>
    {
        public enum Settings
        {
            Database,
            LogPath,
            ExportPath
        }

        private readonly IList<GenFormEnvironment> _genFormEnvironments = new List<GenFormEnvironment>();
        private EnvironmentCollection _environments;

        public GenFormEnvironmentCollection(EnvironmentCollection envCol)
        {
            _environments = envCol;
        }

        public GenFormEnvironmentCollection() {}

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<GenFormEnvironment> GetEnumerator()
        {
            return _genFormEnvironments.GetEnumerator();
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

        public void AddEnvironment(GenFormEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(environment.Database))
                throw new GenFormEnvironmentException("Database connection string cannot be empty");
            _genFormEnvironments.Add(environment);
        }
    }
}