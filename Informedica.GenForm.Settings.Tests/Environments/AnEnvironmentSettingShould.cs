using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.GenForm.Settings.Tests.SettingsManagement;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class AnEnvironmentSettingShould : SecureSettingSourceTestFixture
    {
        [TestMethod]
        public void TranslateMachineEnvironmentProviderAndNameToASettingName()
        {
            SetupSecureSettingSource();

            var settingName = "MyMachine.TestEnvironment.MyDatabase.Provider";
            var machine = "MyMachine";
            var environment = "TestEnvironment";
            var name = "MyDatabase";
            var provider = "Provider";

            var envset = new EnvironmentSetting(machine, environment, name, provider, SecureSettingSource);

            Assert.AreEqual(settingName, envset.SettingName);
        }

        [Isolated]
        [TestMethod]
        public void UseSecureSettingSourceToReadAConnectionString()
        {
            var fakeSetting = Isolate.Fake.Instance<Setting>();
            SetupSecureSettingSource();
            Isolate.WhenCalled(() => SecureSettingSource.SingleOrDefault(s => s.Type == "Conn" && s.Name == Name)).WillReturn(fakeSetting);
            var envset = EnvironmentSetting.CreateEnvironmentSetting("Test", "Test", "Test","Test", SecureSettingSource);

            envset.ConnectionString = "Some connection string";
            var connstr = envset.ConnectionString;
            Assert.AreEqual(connstr, envset.ConnectionString);
            Isolate.Verify.WasCalledWithAnyArguments(
                () => SecureSettingSource.SingleOrDefault(s => s.Type == "Conn" && s.Name == Name));
        }

        [Isolated]
        [TestMethod]
        public void ReturnSameConnectionStringAsSetToConnectionString()
        {
            var fakeSetting = Isolate.Fake.Instance<Setting>();
            SetupSecureSettingSource();
            Isolate.WhenCalled(() => SecureSettingSource.SingleOrDefault(s =>s.Name == Name)).WillReturn(fakeSetting);
            var envset = EnvironmentSetting.CreateEnvironmentSetting("Test", "Test", "Test","Test", SecureSettingSource);

            envset.ConnectionString = "Some connection string";
            var connstr = envset.ConnectionString;
            Assert.AreEqual(connstr, envset.ConnectionString);
        }

        [Isolated]
        [TestMethod]
        public void UseSecureSettingSourceToWriteAConnectionString()
        {
            var fakeSetting = Isolate.Fake.Instance<Setting>();
            SetupSecureSettingSource();
            Isolate.WhenCalled(() => SecureSettingSource.Add(fakeSetting)).IgnoreCall();

            var envset = GetEnvironmentSetting();

            var connstring = "This is a connectionstring";
            envset.ConnectionString = connstring;
            Isolate.Verify.WasCalledWithAnyArguments(() => SecureSettingSource.Add(fakeSetting));

        }

        private EnvironmentSetting GetEnvironmentSetting()
        {
            var machine = "MyMachine";
            var environment = "Test";
            var name = "MyDatabase";
            var provider = "Provider";

            return new EnvironmentSetting(name, machine, environment, provider, SecureSettingSource);
        }

    }
}
