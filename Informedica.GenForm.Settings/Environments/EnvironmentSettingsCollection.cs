using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSettingsCollection : IEnumerable<EnvironmentSetting>
    {
        public const char Separator = '.';

        private readonly SettingsManager _manager;
        private readonly string _machine;
        private readonly string _environment;
        private SecureSettingSource _source;

        public EnvironmentSettingsCollection(SettingsManager manager, string machine, string environment)
        {
            if (manager == null) throw new NullReferenceException("Settings manager cannot be null");

            _manager = manager;
            _machine = machine;
            _environment = environment;
        }

        public EnvironmentSettingsCollection(string machine, string environment, SecureSettingSource source)
        {
            _machine = machine;
            _environment = environment;
            _source = source;
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
            return GetEnvironmentSettings().GetEnumerator();
        }

        private IEnumerator<EnvironmentSetting> GetEnvironments()
        {
            var conns = _manager.GetConnectionStrings();
            IList<EnvironmentSetting> envs = new List<EnvironmentSetting>();
            foreach (var setting in conns)
            {
                GetValue(envs, setting);
            }

            return envs.GetEnumerator();
        }

        private void GetValue(IList<EnvironmentSetting> envs, ConnectionStringSettings setting)
        {
            if (setting.Name.Split(Separator).GetUpperBound(0) < 3) return;

            var name = GetIdFromConnectionString(setting);
            var mach = GetMachineNameFromConnectionString(setting);
            var environment = GetNameFromConnectionString(setting);

            if (_machine == mach && environment == _environment)
                envs.Add(EnvironmentSetting.CreateEnvironmentSetting(name, mach, environment, _manager));
        }

        private IEnumerable<EnvironmentSetting> GetEnvironmentSettings()
        {
            IList<EnvironmentSetting> envs = new List<EnvironmentSetting>();
            foreach (var setting in _source)
            {
                if (setting.Type == ConfigurationSettingSource.Types.Conn.ToString());
                envs.Add(new EnvironmentSetting(GetNameFromSetting(setting), _machine, _environment, string.Empty, setting.Value, _source));
            }

            return envs;
        }

        private string GetNameFromSetting(Setting setting)
        {
            return setting.Name.Split(Separator)[0];
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

        [Obsolete]
        public void AddSetting_Old(string name, string machinename, string environment)
        {
            var setting = EnvironmentSetting.CreateEnvironmentSetting(name, machinename, environment, _manager);
            AddSetting_Old(setting);
        }

        public void AddSetting(string name, string machine, string environment)
        {
            var setting = EnvironmentSetting.CreateEnvironmentSetting(name, machine, environment, _source);
            if (this.Any(s => s.Name == name)) throw new DuplicateSettingError();
            AddSetting(setting);
        }

        private void AddSetting(EnvironmentSetting environmentSetting)
        {
            _source.WriteSecure(new Setting(environmentSetting.SettingName, string.Empty, "Conn", true));
        }

        public void RemoveEnvironmentSetting(string name)
        {
            var setting = EnvironmentSetting.CreateEnvironmentSetting(name, _machine, _environment, _source);            
            _source.Remove(_source.SingleOrDefault(s => s.Name == setting.SettingName));
        }

        [Obsolete]
        public void RemoveEnvironment_Old(EnvironmentSetting environmentSetting)
        {
            _manager.RemoveConnectionString(environmentSetting.SettingName);
        }

        [Obsolete]
        public void AddSetting_Old(EnvironmentSetting setting)
        {
            if (this.Any(s => s.IsIdentical(setting))) throw new DuplicateSettingError();
            _manager.AddConnectionString(setting.SettingName, setting.ConnectionString_Old);
        }
    }
}