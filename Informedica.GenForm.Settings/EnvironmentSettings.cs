using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace Informedica.GenForm.Settings
{
    public class EnvironmentSettings: IEnumerable<EnvironmentSetting>
    {
        private SettingsManager _settingsManager;

        public EnvironmentSettings(SettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public bool Contains(EnvironmentSetting env)
        {
            return _settingsManager.GetConnectionString(env.Name) != null;
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
            return GetEnvironments();
        }

        private IEnumerator<EnvironmentSetting> GetEnvironments()
        {
            var conns = _settingsManager.GetConnectionStrings();
            IList<EnvironmentSetting> envs = new List<EnvironmentSetting>();
            foreach (var setting in conns)
            {
                if (setting.Name.Split('.').GetUpperBound(0) < 2) continue;
             
                var mach = GetMachineNameFromConnectionString(setting);
                var name = GetNameFromConnectionString(setting);
                var prov = GetProviderFromConnectionString(setting);

                envs.Add(new EnvironmentSetting(mach, name, prov, setting.ConnectionString));
            }

            return envs.GetEnumerator();
        }

        private static string GetProviderFromConnectionString(ConnectionStringSettings setting)
        {
            return setting.Name.Split('.')[2];
        }

        private static string GetMachineNameFromConnectionString(ConnectionStringSettings setting)
        {
            return setting.Name.Split('.')[0];
        }

        private static string GetNameFromConnectionString(ConnectionStringSettings setting)
        {
            return setting.Name.Split('.')[1];
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

        public void AddEnvironment(EnvironmentSetting env)
        {
            _settingsManager.AddConnectionString(GetEnvironmentName(env), env.ConnectionString);
        }

        private static string GetEnvironmentName(EnvironmentSetting env)
        {
            return env.MachineName + "." + env.Name + "." + env.Provider;
        }

        public void RemoveEnvironment(EnvironmentSetting env)
        {
            _settingsManager.RemoveConnectionString(GetEnvironmentName(env));
        }
    }
}