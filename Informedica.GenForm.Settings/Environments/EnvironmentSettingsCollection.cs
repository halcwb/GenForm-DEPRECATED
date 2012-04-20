using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentSettingsCollection : IEnumerable<EnvironmentSetting>
    {
        public const char Separator = '.';

        private readonly string _machine;
        private readonly string _environment;
        private SecureSettingSource _source;

        public EnvironmentSettingsCollection(string machine, string environment, SecureSettingSource source)
        {
            _machine = machine;
            _environment = environment;
            _source = source;
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

        private IEnumerable<EnvironmentSetting> GetEnvironmentSettings()
        {
            IList<EnvironmentSetting> envs = new List<EnvironmentSetting>();
            foreach (var setting in _source)
            {
                if (setting.Type == ConfigurationSettingSource.Types.Conn.ToString())
                    envs.Add(new EnvironmentSetting(GetNameFromSetting(setting), _machine, _environment, _source));
            }

            return envs;
        }

        private string GetNameFromSetting(Setting setting)
        {
            return setting.Name.Split(Separator)[0];
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

        public void AddSetting(string name, string machine, string environment)
        {
            var setting = EnvironmentSetting.CreateEnvironmentSetting(name, machine, environment, _source);
            if (this.Any(s => s.Name == name)) throw new DuplicateSettingError("EnvironmentSetting with name " + name + "already exists");
            AddSetting(setting);
        }

        public void AddSetting(string name)
        {
            if (this.Any(envset => envset.Name == name)) throw new DuplicateSettingError("EnvironmentSetting with name " + name + "already exists");
            var setting = EnvironmentSetting.CreateEnvironmentSetting(name, _machine, _environment, _source);
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

    }
}