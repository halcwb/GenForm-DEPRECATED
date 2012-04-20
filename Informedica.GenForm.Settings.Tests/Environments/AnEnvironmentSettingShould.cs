using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.GenForm.Settings.Environments;
using Informedica.GenForm.Settings.Tests.SettingsManagement;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class AnEnvironmentSettingShould : SecureSettingSourceTest
    {
        [TestMethod]
        public void TranslateMachineEnvironmentAndNameToASettingName()
        {
            SetupSecureSettingSource();

            var settingName = "MyDatabase.MyMachine.Test";
            var machine = "MyMachine";
            var environment = "Test";
            var name = "MyDatabase";

            var envset = new EnvironmentSetting(name, machine, environment, _secureSettingSource);

            Assert.AreEqual(settingName, envset.SettingName);
        }

        [Isolated]
        [TestMethod]
        public void UseSecureSettingSourceToReadAConnectionString()
        {
            var fakeSetting = Isolate.Fake.Instance<Setting>();
            SetupSecureSettingSource();
            Isolate.WhenCalled(() => _secureSettingSource.ReadSecure(ConfigurationSettingSource.Types.Conn, null)).WillReturn(fakeSetting);
            var envset = EnvironmentSetting.CreateEnvironmentSetting("Test", "Test", "Test", _secureSettingSource);

            envset.ConnectionString = "Some connection string";
            var connstr = envset.ConnectionString;
            Assert.AreEqual(connstr, envset.ConnectionString);
            Isolate.Verify.WasCalledWithAnyArguments(() => _secureSettingSource.ReadSecure(ConfigurationSettingSource.Types.Conn, null));            
        }

    [Isolated]
        [TestMethod]
        public void UseSecureSettingSourceToWriteAConnectionString()
        {
            var fakeSetting = Isolate.Fake.Instance<Setting>();
            SetupSecureSettingSource();
            Isolate.WhenCalled(() => _secureSettingSource.WriteSecure(fakeSetting)).IgnoreCall();

            var machine = "MyMachine";
            var environment = "Test";
            var name = "MyDatabase";
            var connstring = "This is a connectionstring";

            var envset = new EnvironmentSetting(name, machine, environment, _secureSettingSource);

            envset.ConnectionString = connstring;
            Isolate.Verify.WasCalledWithAnyArguments(() => _secureSettingSource.WriteSecure(fakeSetting));

        }
    }
}
