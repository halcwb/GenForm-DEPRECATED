using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Informedica.GenForm.Settings
{
    public class GenFormEnvironments: IEnumerable<GenFormEnvironment>
    {
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

        public GenFormEnvironment CreateNewEnvironment(string name, string machine)
        {
            const int settingCount = 3;
            var env = Environment.Create(name, machine);
            for (var i = 0; i < settingCount; i++)
            {
                env.Settings.AddSetting(new EnvironmentSetting(i, machine, name, string.Empty, string.Empty));
            }
            return new GenFormEnvironment(env);
        }

        public void AddEnvironment(GenFormEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(environment.GenFormDatabaseConnectionString)) throw new Exception();
            _environments.Add(environment);
        }
    }
}