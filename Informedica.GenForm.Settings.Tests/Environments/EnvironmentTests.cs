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
            [Isolated]
            [TestMethod]
            public void UsesEnvironmentSettingsToGetTheSettings()
            {
                var fakeEnvironmentSettings = Isolate.Fake.Instance<EnvironmentSettingsCollection>();
                Isolate.WhenCalled(() => fakeEnvironmentSettings.Any()).WillReturn(false);
                var env = new Environment("Test", "Test", fakeEnvironmentSettings);

                try
                {
                    Assert.IsTrue(!env.Settings.Any());
                    Isolate.Verify.WasCalledWithAnyArguments(() => fakeEnvironmentSettings.Any());
                }
                catch (Exception e)
                {
                    Assert.Fail(e.ToString());
                }
            }

            [TestMethod]
            public void HasCountZeroWhenEnvironmentIsNew()
            {
                var env = new Environment("Test", "Test");

                Assert.IsTrue(!env.Settings.Any());
            }
        }
    }
}
