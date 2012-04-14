using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Informedica.GenForm.Settings
{
    public class EnvironmentSettings: IEnumerable<EnvironmentSetting>
    {
        public const char Separator = '.';

        private readonly SettingsManager _manager;
        private readonly string _machine;
        private readonly string _environment;

        public EnvironmentSettings(SettingsManager manager, string machine, string environment)
        {
            if (manager == null) throw  new NullReferenceException("Settings manager cannot be null");

            _manager = manager;
            _machine = machine;
            _environment = environment;
        }

        public bool Contains(EnvironmentSetting env)
        {
            return _manager.GetConnectionString(env.Environment) != null;
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
            var conns = _manager.GetConnectionStrings();
            IList<EnvironmentSetting> envs = new List<EnvironmentSetting>();
            foreach (var setting in conns)
            {
                if (setting.Name.Split(Separator).GetUpperBound(0) < 3) continue;

                var name = GetIdFromConnectionString(setting);
                var mach = GetMachineNameFromConnectionString(setting);
                var environment = GetNameFromConnectionString(setting);

                if (_machine == mach && environment == _environment) envs.Add(EnvironmentSetting.CreateEnvironment(name, mach, environment, _manager));
            }

            return envs.GetEnumerator();
        }

        private static string GetIdFromConnectionString(ConnectionStringSettings setting)
        {
            return setting.Name.Split(Separator)[0];
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

        public void AddSetting(string name, string machinename, string environment)
        {
            var setting = EnvironmentSetting.CreateEnvironment(name, machinename, environment, _manager);
            AddSetting(setting);
        }

        public void RemoveEnvironment(EnvironmentSetting setting)
        {
            _manager.RemoveConnectionString(setting.SettingName);
        }

        public void AddSetting(EnvironmentSetting setting)
        {
            if (this.Any(s => s.IsIdentical(setting))) throw new DuplicateSettingError();
            _manager.AddConnectionString(setting.SettingName, setting.ConnectionString);
        }
    }

    public class DuplicateSettingError : Exception
    {
    }
}