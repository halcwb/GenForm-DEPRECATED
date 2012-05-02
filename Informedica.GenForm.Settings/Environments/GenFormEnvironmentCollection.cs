using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Informedica.GenForm.Settings.Environments
{
    public class GenFormEnvironmentCollection: ICollection<GenFormEnvironment>
    {
        private IList<GenFormEnvironment> _genFormEnvironments = new List<GenFormEnvironment>();
        private EnvironmentCollection _environments;

        public GenFormEnvironmentCollection(EnvironmentCollection environments)
        {
            _environments = environments;
        }

        private void AddEnvironment(GenFormEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(environment.Database))
                throw new GenFormEnvironmentException("Database connection string cannot be empty");

            _environments.Add(environment.Environmnent);
        }


        #region Implementation of ICollection<GenFormEnvironment>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<GenFormEnvironment> GetEnumerator()
        {
            RefreshEnvironments();
            return _genFormEnvironments.GetEnumerator();
        }

        private void RefreshEnvironments()
        {
            _genFormEnvironments = new List<GenFormEnvironment>();

            foreach (var environment in _environments)
            {
                if (IsGenFormEnvironment(environment)) _genFormEnvironments.Add(new GenFormEnvironment(environment));
            }
        }

        private static bool IsGenFormEnvironment(Environment environment)
        {
            return environment.Settings.Any(s => s.Name == "Database") &&
                   environment.Settings.Any(s => s.Name == "LogPath") &&
                   environment.Settings.Any(s => s.Name == "ExportPath");
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


        public void Add(GenFormEnvironment item)
        {
            AddEnvironment(item);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(GenFormEnvironment item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(GenFormEnvironment[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(GenFormEnvironment item)
        {
            throw new System.NotImplementedException();
        }

        public int Count
        {
            get
            {
                RefreshEnvironments();
                return _genFormEnvironments.Count;
            }
        }

        public bool IsReadOnly
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}