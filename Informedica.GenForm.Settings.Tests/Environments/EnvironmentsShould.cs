using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings;
using Informedica.SecureSettings.Sources;
using Informedica.SecureSettings.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class EnvironmentsShould
    {

        private static EnvironmentSetting GetFakeEnvironmentSetting()
        {
            var env = Isolate.Fake.Instance<EnvironmentSetting>();
            Isolate.WhenCalled(() => env.Name).WillReturn("test");
            Isolate.WhenCalled(() => env.MachineName).WillReturn("test");
            Isolate.WhenCalled(() => env.Environment).WillReturn("test");
            Isolate.WhenCalled(() => env.Provider).WillReturn("test");
            Isolate.WhenCalled(() => env.ConnectionString).WillReturn("test");

            return env;
        }

        [Isolated]
        [TestMethod]
        public void NotAcceptTheSameSettingTwice()
        {
            var envs = GetEnvironments();
            var setting = GetFakeEnvironmentSetting();
            Isolate.WhenCalled(() => setting.IsIdentical(setting)).WillReturn(true);

            envs.AddSetting_Old(setting);

            try
            {
                envs.AddSetting_Old(setting);
                Assert.Fail("should not accept the same setting twice");
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException), e.ToString());
            }

        }

        [Isolated]
        [TestMethod]
        public void WhenThereAreNoEnvironmentsReturnZeroEnvironmentCount()
        {
            var envs = GetEnvironmentSettingsWithCountZero();

            Assert.AreEqual(0, envs.Count());
        }

        private static IEnumerable<EnvironmentSetting> GetEnvironmentSettingsWithCountZero()
        {
            var setman = Isolate.Fake.Instance<SettingsManager>();
            Isolate.WhenCalled(() => setman.GetConnectionStrings()).DoInstead(ReturnEmptyConnectionStringList);

            return new EnvironmentSettingsCollection(setman, "Test", "Test");
        }

        private static IEnumerable<ConnectionStringSettings> ReturnEmptyConnectionStringList(MethodCallContext arg)
        {
            return new List<ConnectionStringSettings>();
        }

        [Isolated]
        [TestMethod]
        public void BeAbleToAddAnTestEnvironment()
        {
            var env = GetFakeEnvironmentSetting();
            var envs = GetEnvironments();

            try
            {
                envs.AddSetting_Old(env);

            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void BeAbleToRemoveAnTestEnvironment()
        {
            var envs = GetEnvironments();
            var env = GetFakeEnvironmentSetting();

            envs.AddSetting_Old(env);

            try
            {
                envs.RemoveEnvironment(env);

            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void HaveTestEnvironmentWhenTestEnvironmentIsAdded()
        {
            const string machine = "test";
            const string environment = "test";

            var man = CreateIsolatedSettingsManager();
            var setting = new EnvironmentSetting("test", machine, environment, "test", "test", man);

            var envs = new EnvironmentSettingsCollection(man, machine, environment);
            envs.AddSetting_Old(setting);

            Assert.IsTrue(envs.Any(e => e.Environment == setting.Environment));
        }

        private static SettingsManager CreateIsolatedSettingsManager()
        {
            var source = new TestSettingSource();
            var man = new SettingsManager(new SecureSettingSource(source));
            return man;
        }

        [Isolated]
        [TestMethod]
        public void BeAbleToRemoveTestEnvironmentAfterTestEnvironmentIsAdded()
        {
            var source = new TestSettingSource();
            var envs = new EnvironmentSettingsCollection(new SettingsManager(new SecureSettingSource(source)), "test", "test");
            var env = new EnvironmentSetting("test", "test", "test", "test", "test", CreateIsolatedSettingsManager());
            envs.AddSetting_Old(env);
            envs.RemoveEnvironment(env);

            Assert.IsFalse(envs.Any(e => e.Environment == env.Environment));
        }

        private static EnvironmentSettingsCollection GetEnvironments()
        {
            var setman = GetFakeSettingsManager();

            return new EnvironmentSettingsCollection(setman, "Test", "Test");
        }

        private static SettingsManager GetFakeSettingsManager()
        {
            var setman = Isolate.Fake.Instance<SettingsManager>();
            Isolate.WhenCalled(() => setman.AddConnectionString("test.test.test", "test")).IgnoreCall();
            Isolate.WhenCalled(() => setman.RemoveConnectionString("test.test.test")).IgnoreCall();

            return setman;
        }
    }
}
