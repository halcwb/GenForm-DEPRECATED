using System;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.SettingsManagement
{
    [TestClass]
    public class AConfigurationSettingSourceShould
    {
        private SettingSource _configSettingSource;
        private Configuration _configuration;
        private string _settingName;
        private KeyValueConfigurationCollection _settings;
        private ConnectionStringSettingsCollection _connections;
        private ConfigurationFactory _factory;

        private void SetUpGenFormWebConfiguration()
        {
            Isolate.CleanUp();
            _configuration = WebConfigurationManager.OpenWebConfiguration("/GenForm");
            _factory = Isolate.Fake.Instance<ConfigurationFactory>();
            Isolate.WhenCalled(() => _factory.GetConfiguration()).WillReturn(_configuration);

            _configSettingSource = new ConfigurationSettingSource(_factory);
        }

        private ISetting WriteAppSetting()
        {
            var el = new KeyValueConfigurationElement("Test", "Test");
            Isolate.WhenCalled(() => el.Key).WillReturn("Test");
            var setting = SettingFactory.CreateSecureSetting(el);
            _configSettingSource.Add(setting);
            return setting;
        }

        [TestInitialize]
        public void IsolateTheWebSettingSource()
        {
            _settingName = "Test";
            _configuration = Isolate.Fake.Instance<Configuration>();

            _settings = Isolate.Fake.Instance<KeyValueConfigurationCollection>();
            // Note: have to fake the keyvalueconfiguration element, when actually creating it
            // using constructor(key, value), after creation element.key returns empty string???
            var fakeElement = Isolate.Fake.Instance<KeyValueConfigurationElement>();
            Isolate.WhenCalled(() => fakeElement.Key).WillReturn(_settingName);
            Isolate.WhenCalled(() => _settings[_settingName]).WillReturn(fakeElement);
            var empty = new[] {_settingName};
            Isolate.WhenCalled(() => _settings.AllKeys).WillReturn(empty);

            _connections = Isolate.Fake.Instance<ConnectionStringSettingsCollection>();
            var fakeConnection = Isolate.Fake.Instance<ConnectionStringSettings>();
            Isolate.WhenCalled(() => fakeConnection.Name).WillReturn(_settingName);
            Isolate.WhenCalled(() => _connections[_settingName]).WillReturn(fakeConnection);

            Isolate.WhenCalled(() => _configuration.AppSettings.Settings).WillReturn(_settings);
            Isolate.WhenCalled(() => _configuration.ConnectionStrings.ConnectionStrings).WillReturn(_connections);

            _factory = Isolate.Fake.Instance<ConfigurationFactory>();
            Isolate.WhenCalled(() => _factory.GetConfiguration()).WillReturn(_configuration);

            _configSettingSource = new ConfigurationSettingSource(_factory);
        }

        [Isolated]
        [TestMethod]
        public void CallConfigurationFactoryGetConfigurationToGetAConfiguration()
        {
            try
            {
                var check = _configSettingSource.Any();
                Assert.IsFalse(check);
                Isolate.Verify.WasCalledWithAnyArguments(() => _factory.GetConfiguration());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void UseAConfigurationToGetTheAppSettings()
        {
            try
            {
                var check = _configSettingSource.Any();
                Assert.IsFalse(check);
                Isolate.Verify.WasCalledWithAnyArguments(() => _configuration.AppSettings.Settings);

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
                var check = _configSettingSource.Any();
                Assert.IsFalse(check);
                Isolate.Verify.WasCalledWithAnyArguments(() => _configuration.ConnectionStrings.ConnectionStrings);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
            
        }

        [Isolated]
        [TestMethod]
        public void CallConfigurationAppSettingsToReadAnAppSetting()
        {
            try
            {
                ReadSetting(typeof(KeyValueConfigurationElement), _settingName);
                Isolate.Verify.WasCalledWithAnyArguments(() => _configuration.AppSettings);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void CallConfigurationConnectionStringsToReadAnConnectionSetting()
        {
            try
            {
                ReadSetting(typeof(ConnectionStringSettings), _settingName);
                Isolate.Verify.WasCalledWithAnyArguments(() => _configuration.ConnectionStrings);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void UseConfigurationAppSettingsToWriteAnAppSetting()
        {
            try
            {
                WriteAppSetting();
                Isolate.Verify.WasCalledWithExactArguments(() => _configuration.AppSettings);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }            
        }

        [Isolated]
        [TestMethod]
        public void CallSaveOnConfigurationToSaveTheSettings()
        {
            Isolate.WhenCalled(() => _configuration.Save()).IgnoreCall();

            try
            {
                _configSettingSource.Save();
                Isolate.Verify.WasCalledWithAnyArguments(() => _configuration.Save());

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void ContainAnTestAppSettingAfterWritingTheTestAppSetting()
        {
            SetUpGenFormWebConfiguration();
            var setting = WriteAppSetting();

            Assert.IsTrue(_configSettingSource.Any(s => s.Key == setting.Key));
        }

        [Isolated]
        [TestMethod]
        public void UseConfigurationConnectionStringsToWriteAConnectionString()
        {
            try
            {
                var setting = SettingFactory.CreateSecureSetting(new ConnectionStringSettings("Test", "Test"));
                _configSettingSource.Add(setting);
                Isolate.Verify.WasCalledWithExactArguments(() => _configuration.ConnectionStrings);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void UseConfigurationToRemoveAnAppSetting()
        {
            try
            {
                var setting = WriteAppSetting();
                _configSettingSource.Remove(setting);
                Isolate.Verify.WasCalledWithExactArguments(() => _configuration.AppSettings);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void UseConfigurationToRemoveAConnectionString()
        {
            try
            {
                var setting = SettingFactory.CreateSecureSetting(new ConnectionStringSettings("Test", "Test"));
                _configSettingSource.Remove(setting);
                Isolate.Verify.WasCalledWithExactArguments(() => _configuration.ConnectionStrings);

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void ReturnNullWhenTryingToGetANonExistingSetting()
        {
            SetUpGenFormWebConfiguration();

            var setting = _configSettingSource.SingleOrDefault(s => s.Key == "FooBar");
            Assert.IsNull(setting);
        }

        [Isolated]
        [TestMethod]
        public void ReturnANullValueWhenSettingCannotBeRead()
        {
            SetUpGenFormWebConfiguration();
            var setting = WriteAppSetting();
            Assert.IsNotNull(ReadSetting(typeof(KeyValueConfigurationElement), setting.Key));

            _configSettingSource.Remove(setting);
            setting = ReadSetting(typeof(KeyValueConfigurationElement), setting.Key);
            Assert.IsNull(setting);
        }

        [Isolated]
        [TestMethod]
        public void HaveTheSameAmountOfSettingsAsInTheSettingsFile()
        {
            var config = (new WebConfigurationFactory()).GetConfiguration();
            var count = config.ConnectionStrings.ConnectionStrings.Count;
            count += config.AppSettings.Settings.Count;

            var source = SettingSourceFactory.GetSettingSource();

            Assert.AreEqual(count, source.Count);
        }

        private ISetting ReadSetting(Type type, string name)
        {
            return _configSettingSource.SingleOrDefault(s => s.Type == type && s.Key == name);
        }

    }
}
