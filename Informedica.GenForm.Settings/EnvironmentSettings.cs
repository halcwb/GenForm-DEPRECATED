using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace Informedica.GenForm.Settings
{
    public class EnvironmentSettings: IEnumerable<EnvironmentSetting>
    {
        public const char Separator = '.';

        private readonly SettingsManager _settingsManager;
        private readonly string _machine;
        private readonly string _environment;

        public EnvironmentSettings(SettingsManager settingsManager, string machine, string environment)
        {
            _settingsManager = settingsManager;
            _machine = machine;
            _environment = environment;
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
                if (setting.Name.Split(Separator).GetUpperBound(0) < 3) continue;

                var id = GetIdFromConnectionString(setting);
                var mach = GetMachineNameFromConnectionString(setting);
                var name = GetNameFromConnectionString(setting);
                var prov = GetProviderFromConnectionString(setting);

                if (_machine == mach && name == _environment) envs.Add(new EnvironmentSetting(id, mach, name, prov, setting.ConnectionString));
            }

            return envs.GetEnumerator();
        }

        private static int GetIdFromConnectionString(ConnectionStringSettings setting)
        {
            return int.Parse(setting.Name.Split(Separator)[0]);
        }

        private static string GetProviderFromConnectionString(ConnectionStringSettings setting)
        {
            return setting.Name.Split(Separator)[3];
        }

        private static string GetMachineNameFromConnectionString(ConnectionStringSettings setting)
        {
            return setting.Name.Split(Separator)[1];
        }

        private static string GetNameFromConnectionString(ConnectionStringSettings setting)
        {
            return setting.Name.Split(Separator)[2];
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

        public void AddSetting(EnvironmentSetting setting)
        {
            if (this.Any(set => set.IsIdentical(setting))) throw new DuplicateSettingError();
            _settingsManager.AddConnectionString(GetEnvironmentName(setting), setting.ConnectionString);
        }

        private static string GetEnvironmentName(EnvironmentSetting env)
        {
            return env.Id.ToString(CultureInfo.InvariantCulture) + Separator + env.MachineName + Separator + env.Name + Separator + env.Provider;
        }

        public void RemoveEnvironment(EnvironmentSetting env)
        {
            _settingsManager.RemoveConnectionString(GetEnvironmentName(env));
        }
    }

    public class DuplicateSettingError : Exception
    {
    }
}