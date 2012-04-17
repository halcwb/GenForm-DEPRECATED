using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests
{
    [TestClass]
    public class AConfigurationSettingSourceShould
    {
        private SettingSource _configSettingSource;
        private Configuration _configarion;
        private string _settingName;
        private KeyValueConfigurationCollection _settings;

        [TestInitialize]
        public void IsolateTheWebSettingSource()
        {
            _settingName = "Test";
            _configarion = Isolate.Fake.Instance<Configuration>();

            _settings = Isolate.Fake.Instance<KeyValueConfigurationCollection>();
            var fakeElement = Isolate.Fake.Instance<KeyValueConfigurationElement>();
            Isolate.WhenCalled(() => fakeElement.Key).WillReturn(_settingName);
            Isolate.WhenCalled(() => _settings[_settingName]).WillReturn(fakeElement);

            Isolate.WhenCalled(() => _configarion.AppSettings.Settings).WillReturn(_settings);
            Isolate.WhenCalled(() => _configarion.ConnectionStrings.ConnectionStrings).ReturnRecursiveFake();

            _configSettingSource = new ConfigarionSettingSource(_configarion);
        }

        [Isolated]
        [TestMethod]
        public void UseAConfigarionToGetTheAppSettings()
        {
            try
            {
                _configSettingSource.Any();
                Isolate.Verify.WasCalledWithAnyArguments(() => _configarion.AppSettings.Settings);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void UseConfigurationToGetTheConnectionStrings()
        {
            try
            {
                _configSettingSource.Any();
                Isolate.Verify.WasCalledWithAnyArguments(() => _configarion.ConnectionStrings.ConnectionStrings);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
            
        }

        [Isolated]
        [TestMethod]
        public void CallConfigationAppSettingsToReadAnAppSetting()
        {
            try
            {
                _configSettingSource.ReadSetting(ConfigarionSettingSource.Types.App, _settingName);
                Isolate.Verify.WasCalledWithExactArguments(() => _settings[_settingName]);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }
    }

    public class ConfigarionSettingSource : SettingSource
    {
        private readonly Configuration _configuration;

        #region Overrides of SettingSource

        public ConfigarionSettingSource(Configuration configuration)
        {
            _configuration = configuration;
        }

        public ConfigarionSettingSource(Configuration configuration,
                                        IDictionary<Enum, Action<Setting>> writers, 
                                        IDictionary<Enum, Func<string, Setting>> readers, 
                                        IDictionary<Enum, Action<Setting>> removers) : 
            base(writers, readers, removers)
        {
            _configuration = configuration;
        }

        protected override Enum SettingTypeToString(Setting setting)
        {
            throw new NotImplementedException();
        }

        protected override void RegisterReaders()
        {
            Readers.Add(Types.App, ReadAppSetting);
        }

        protected override void RegisterWriters()
        {
        }

        protected override void RegisterRemovers()
        {
        }

        protected override IEnumerable<Setting> Settings
        {
            get
            {
                var list = new List<Setting>();
                list.AddRange(ReadAppSettings());
                list.AddRange(ReadConnectionStrings());
                return list;
            }
        }

        private IEnumerable<Setting> ReadConnectionStrings()
        {
            return (from ConnectionStringSettings connstring in _configuration.ConnectionStrings.ConnectionStrings
                    select new Setting(connstring.Name, connstring.ConnectionString, "Conn", false)).ToList();
        }

        private IEnumerable<Setting> ReadAppSettings()
        {
            return (from KeyValueConfigurationElement setting in _configuration.AppSettings.Settings 
                    select new Setting(setting.Key, setting.Value, "App", false)).ToList();
        }

        #endregion

        public Setting ReadAppSetting(string name)
        {
            return CreateSetting(_configuration.AppSettings.Settings[name]);
        }

        private static Setting CreateSetting(KeyValueConfigurationElement element)
        {
            return new Setting(element.Key, element.Value, Types.App.ToString(), false);
        }

        public enum Types
        {
            App
        }
    }
}
