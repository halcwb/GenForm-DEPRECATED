using System;
using Informedica.GenForm.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Acceptance.Tests
{
    [TestClass]
    public class ANewEnvironmentSettingShould
    {
        private static string _connectionString;
        private static string _name;
        private static string _provider;
        private static string _machineName;

        [TestMethod]
        public void HaveAMachineName()
        {
            var mach = System.Environment.MachineName;
            var env = CreateEnvironmentSetting();
            Assert.AreEqual(mach, env.MachineName);
        }

        [TestMethod]
        public void HaveEnvironmentNameConsistingOfMachineNameAndProviderName()
        {
            var env = CreateEnvironmentSetting();
            
            Assert.AreEqual(env.MachineName + "." + env.Name + "." + env.Provider, env.SettingName);
        }

        private static EnvironmentSetting CreateEnvironmentSetting()
        {
            _machineName = System.Environment.MachineName;
            _provider = "SqLite";
            _name = "Test";
            _connectionString = "Data Source=:memory:;Version=3;New=True;Pooling=True;Max Pool Size=1;";

            var env = new EnvironmentSetting(_machineName, _provider, _name, _connectionString);

            return env;
        }

        [TestMethod]
        public void HaveAConnectionString()
        {
            var env = CreateEnvironmentSetting();

            Assert.AreEqual(_connectionString, env.ConnectionString);
        }

        public void BeAddedToEnvironmnentManager()
        {
            var env = CreateEnvironmentSetting();
            var man = GetEnvironmentManager();

            Assert.IsTrue(man.Contains(env));
        }

        private static EnvironmentSettings GetEnvironmentManager()
        {
            return new EnvironmentSettings(SettingsManager.Instance);
        }
    }
}