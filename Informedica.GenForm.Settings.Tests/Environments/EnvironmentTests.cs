using System;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using Environment = Informedica.GenForm.Settings.Environments.Environment;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class EnvironmentTests
    {
        private EnvironmentSettingsCollection _fakeEnvironmentSettings;

        [TestMethod]
        public void ThatNameHasAValueWhenEnvironmentIsCreated()
        {
            var env = Environment.Create("Test", "Test");
            Assert.AreEqual("Test", env.Name);
        }

        [TestMethod]
        public void ThatMachineIsSetToLocalMachineNameWhenNotExplicitlySetInConstructor()
        {
            var env = Environment.Create("Test", System.Environment.MachineName);
            Assert.AreEqual(System.Environment.MachineName, env.MachineName);
        }

        [TestMethod]
        public void MachineNameIsSetToMacineNameSuppliedInConstructor()
        {
            var machine = "MyMachine";
            var env = new Environment("Test", machine);

            Assert.AreEqual(machine, env.MachineName);
        }

        [Isolated]
        [TestMethod]
        public void SettingsUsesEnvironmentSettingsToGetTheSettings()
        {
            SetupFakeEnvironmentSettings();
            var env = new Environment("Test", "Test", _fakeEnvironmentSettings);

            try
            {
                Assert.IsTrue(!env.Settings.Any());
                Isolate.Verify.WasCalledWithAnyArguments(() => _fakeEnvironmentSettings.Any());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void ThatSettingsHasCountZeroWhenEnvironmentIsNew()
        {
            SetupFakeEnvironmentSettings();
            var env = new Environment("Test", "Test", _fakeEnvironmentSettings);

            Assert.IsTrue(!env.Settings.Any());
        }

        private void SetupFakeEnvironmentSettings()
        {
            _fakeEnvironmentSettings = Isolate.Fake.Instance<EnvironmentSettingsCollection>();
            Isolate.WhenCalled(() => _fakeEnvironmentSettings.Any()).WillReturn(false);
        }

    }
}
