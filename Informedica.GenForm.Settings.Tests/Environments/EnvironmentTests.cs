using System;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using Environment = Informedica.GenForm.Settings.Environments.Environment;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    public class EnvironmentTests
    {
        [TestClass]
        public class ThatNameProperty
        {
            [TestMethod]
            public void HasAValueWhenEnvironmentIsCreated()
            {
                var env = Environment.Create("Test", "Test");
                Assert.AreEqual("Test", env.Name);
            }
        }

        [TestClass]
        public class ThatMachineNameProperty
        {
            [TestMethod]
            public void IsSetToLocalMachineNameWhenNotExplicitlySetInConstructor()
            {
                var env = Environment.Create("Test", System.Environment.MachineName);
                Assert.AreEqual(System.Environment.MachineName, env.MachineName);
            }

            [TestMethod]
            public void IsSetToNameSuppliedInConstructor()
            {
                var machine = "MyMachine";
                var env = new Environment("Test", machine);

                Assert.AreEqual(machine, env.MachineName);
            }
        }

        [TestClass]
        public class ThatSettingsProperty
        {
            private EnvironmentSettingsCollection _fakeEnvironmentSettings;

            [Isolated]
            [TestMethod]
            public void UsesEnvironmentSettingsToGetTheSettings()
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

            private void SetupFakeEnvironmentSettings()
            {
                _fakeEnvironmentSettings = Isolate.Fake.Instance<EnvironmentSettingsCollection>();
                Isolate.WhenCalled(() => _fakeEnvironmentSettings.Any()).WillReturn(false);
            }

            [TestMethod]
            public void HasCountZeroWhenEnvironmentIsNew()
            {
                SetupFakeEnvironmentSettings();
                var env = new Environment("Test", "Test", _fakeEnvironmentSettings);
                
                Assert.IsTrue(!env.Settings.Any());
            }
        }
    }
}
