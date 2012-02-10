using System;
using System.Linq;
using Informedica.GenForm.Acceptance.Utilities;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Acceptance.FitNesse
{
    public class EnvironmentScenarios: TryCatchTestMethod
    {
        public static ANewEnvironmentSettingShould Should = new ANewEnvironmentSettingShould();

        public string GetMachineName()
        {
            return TryCatch(Should.HaveAMachineName) ? 
                Environment.MachineName: string.Empty;
        }

        public bool EnvironmentNameShouldBe(string name)
        {
            return TryCatch(Should.HaveEnvironmentNameConsistingOfMachineNameAndProviderName);
        }

        public bool EnvironmentConnectionStringCanBeSet(string connString)
        {
            return TryCatch(Should.HaveAConnectionString);
        }

        public bool AddEnvironmentToEnvironmentManager()
        {
            return TryCatch(Should.BeAddedToEnvironmnentManager);
        }

        public bool CurrentMachineHasProvider(string provider)
        {
            var providers = GenFormApplication.GetRegisterdProviders();
            return providers.Any(p => p.ProviderName == provider);
        }

        public bool RegisterEnvironmentWithNameAndProviderWithConnectionString(string name, string provider, string connectionString)
        {
            if (!GenFormApplication.GetRegisterdProviders().Any(p => p.ProviderName == provider)) return false;

            var env = new EnvironmentSetting(Environment.MachineName, name, provider, connectionString);
            GenFormApplication.Environments.AddEnvironment(env);

            return true;
        }
    }

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
            var mach = Environment.MachineName;
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
            _machineName = Environment.MachineName;
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
