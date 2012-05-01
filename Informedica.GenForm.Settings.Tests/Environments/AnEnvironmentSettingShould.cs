using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class AnEnvironmentSettingShould
    {
        private EnvironmentSetting _envsetting;
        private string _provider;
        private string _name;
        private string _environment;
        private string _machineName;
        private string _settingName;

        [TestInitialize]
        public void SetupIsolatedEnvironmentSetting()
        {
            _settingName = "MyMachine.TestEnvironment.MyDatabase.SqLite";
            _machineName = "MyMachine";
            _environment = "TestEnvironment";
            _name = "MyDatabase";
            _provider = "SqLite";

            _envsetting = new EnvironmentSetting(_machineName, _environment, _name, _provider, Isolate.Fake.Instance<ISetting>());
        }

        [Isolated]
        [TestMethod]
        public void HaveMachineNameMyMachineFromSettingKey()
        {
            Assert.AreEqual(_machineName, _envsetting.MachineName);
        }


        [Isolated]
        [TestMethod]
        public void HaveEnvironmentTestEnvironmentFromSettingKey()
        {
            Assert.AreEqual(_environment, _envsetting.Environment);
        }

        [Isolated]
        [TestMethod]
        public void HaveNameMyDatabaseFromSettingKey()
        {
            Assert.AreEqual(_name, _envsetting.Name);
        }

        [Isolated]
        [TestMethod]
        public void HaveProviderSqLiteFromSettingKey()
        {
            Assert.AreEqual(_provider, _envsetting.Provider);
        }
        [Isolated]
        [TestMethod]
        public void TranslateMachineEnvironmentProviderAndNameToASettingName()
        {
            SetupIsolatedEnvironmentSetting();

            Assert.AreEqual(_settingName, _envsetting.SettingName);
        }


        [Isolated]
        [TestMethod]
        public void UseASecureSettingToReadAConnectionString()
        {
        }

        [Isolated]
        [TestMethod]
        public void ReturnSameConnectionStringAsSetToConnectionString()
        {
        }

        [Isolated]
        [TestMethod]
        public void UseSecureSettingSourceToWriteAConnectionString()
        {
        }
    }
}
