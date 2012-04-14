using System;
using System.Collections;
using System.Collections.Generic;

namespace Informedica.GenForm.Settings
{
    public class GenFormEnvironments: IEnumerable<GenFormEnvironment>
    {
        public enum Settings
        {
            Database,
            LogPath,
            ExportPath
        }

        private IList<GenFormEnvironment> _environments = new List<GenFormEnvironment>();

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

        public static GenFormEnvironment CreateNewEnvironment(string name, string machine)
        {
            var env = Environment.Create(name, machine);
            env.Settings.AddSetting(Settings.Database.ToString(), machine, name);
            env.Settings.AddSetting(Settings.ExportPath.ToString(), machine, name);
            env.Settings.AddSetting(Settings.LogPath.ToString(), machine, name);
            
            return new GenFormEnvironment(env);
        }

        public void AddEnvironment(GenFormEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(environment.Database))
                throw new GenFormEnvironmentException("Database connection string cannot be empty");
            _environments.Add(environment);
        }
    }

    public class GenFormEnvironmentException : Exception
    {
        public GenFormEnvironmentException(string message): base(message)
        {
            
        }
    }
}