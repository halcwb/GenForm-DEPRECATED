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

        [TestInitialize]
        public void IsolateTheWebSettingSource()
        {
            _configarion = Isolate.Fake.Instance<Configuration>();
            Isolate.WhenCalled(() => _configarion.AppSettings).ReturnRecursiveFake();
            Isolate.WhenCalled(() => _configarion.ConnectionStrings).ReturnRecursiveFake();

            var writers = new Dictionary<Enum, Action<Setting>>();
            var readers = new Dictionary<Enum, Func<string, Setting>>();
            var removers = new Dictionary<Enum, Action<Setting>>();
            _configSettingSource = new ConfigarionSettingSource(_configarion, writers, readers, removers);
        }

        [TestMethod]
        public void UseAConfigarionToGetTheAppSettings()
        {
            try
            {
                _configSettingSource.Any();
                Isolate.Verify.WasCalledWithAnyArguments(() => _configarion.AppSettings);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void UseConfigurationToGetTheConnectionStrings()
        {
            try
            {
                _configSettingSource.Any();
                Isolate.Verify.WasCalledWithAnyArguments(() => _configarion.ConnectionStrings);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
            
        }
    }

    public class ConfigarionSettingSource : SettingSource
    {
        private Configuration _configuration;

        #region Overrides of SettingSource

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
                var list = ReadAppSettings();
                list.AddRange(ReadConnectionStrings());
                return list;
            }
        }

        private List<Setting> ReadConnectionStrings()
        {
            return (from ConnectionStringSettings connstring in _configuration.ConnectionStrings.ConnectionStrings
                    select new Setting(connstring.Name, connstring.ConnectionString, "Conn", false)).ToList();
        }

        private List<Setting> ReadAppSettings()
        {
            return (from KeyValueConfigurationElement setting in _configuration.AppSettings.Settings 
                    select new Setting(setting.Key, setting.Value, "App", false)).ToList();
        }

        #endregion
    }
}
