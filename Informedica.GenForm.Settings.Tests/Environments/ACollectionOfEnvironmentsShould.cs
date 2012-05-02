using System.Collections.Generic;
using System;
using System.Configuration;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class ACollectionOfEnvironmentsShould
    {
        private TestSource _source;
        private ICollection<EnvironmentSetting> _settings;

        [Isolated]
        [TestMethod]
        public void UseAnEnvironmentSettingSourceToEnumerateThroughEnvironments()
        {
            _source = new TestSource();
            _settings = new EnvironmentSettingsCollection(_source);
            var col = new EnvironmentCollection(_settings);

            try
            {
                Isolate.WhenCalled(() => _settings.GetEnumerator()).CallOriginal();
                Assert.IsFalse(col.Any(s => s.Name == "TestEnvironment"));
                Isolate.Verify.WasCalledWithAnyArguments(() => _settings.GetEnumerator());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void ContainOneEnvironmnentAsSpecifiedInTheTestSource()
        {
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test1.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test2.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test3.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test4.Provider", "Test")
                             };
            _settings = new EnvironmentSettingsCollection(_source);
            var col = new EnvironmentCollection(_settings);

            Assert.AreEqual(1, col.Count);
        }

        [TestMethod]
        public void ContainThreeEnvironmnentSettingsForTestMachine1AndEnv2AsSpecifiedInTheUnsortedTestSource()
        {
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test1.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test1.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.Env1.Test1.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.Env2.Test2.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test2.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test3.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test2.Provider", "Test")
                             };

            _settings = new EnvironmentSettingsCollection(_source);
            var col = new EnvironmentCollection(_settings);

            Assert.AreEqual(3, col.Single(e => e.MachineName == "TestMachine1" && e.Name == "Env2").Settings.Count());
        }

        [TestMethod]
        public void ContainFourEnvironmnentsForTestMachine1AsSpecifiedInTheUnsortedTestSource()
        {
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test1.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test1.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.Env1.Test1.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.Env2.Test2.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test2.Provider", "Test")
                             };

            _settings = new EnvironmentSettingsCollection(_source);
            var col = new EnvironmentCollection(_settings);

            Assert.AreEqual(4, col.Count());
        }

    }
}
