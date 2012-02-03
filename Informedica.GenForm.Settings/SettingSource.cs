using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using Informedica.SecureSettings;

namespace Informedica.GenForm.Settings
{
    public class SettingSource: ISettingSource
    {
        private static readonly IList<Setting> Settings = new List<Setting>();
        private static readonly Configuration Configuration = WebConfigurationManager.OpenWebConfiguration("/GenForm");

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<Setting> GetEnumerator()
        {
            Settings.Clear();
            foreach (ConnectionStringSettings connstr in Configuration.ConnectionStrings.ConnectionStrings)
            {
                Settings.Add(new Setting(connstr.Name, connstr.ConnectionString));
            }
            return Settings.GetEnumerator();
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

        #region Implementation of ISettingSource

        public void WriteConnectionString(string name, string connectionString)
        {
            var conset = Configuration.ConnectionStrings.ConnectionStrings[name];
            if (conset == null)
            {
                conset = new ConnectionStringSettings(name, connectionString);
                Configuration.ConnectionStrings.ConnectionStrings.Add(conset);
            }
            else
            {
                Configuration.ConnectionStrings.ConnectionStrings[name].ConnectionString = connectionString;
            }
            Configuration.Save();
        }

        public string ReadConnectionString(string name)
        {
            return Configuration.ConnectionStrings.ConnectionStrings[name].ConnectionString;
        }

        public void WriteAppSetting(string name, string setting)
        {
            throw new NotImplementedException();
        }

        public string ReadAppSetting(string name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}