using System;
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
        [TestMethod]
        public void UseASettingSourceToEnumerateThroughEnvironments()
        {
            var key = "TestMachine.TestEnvironment.Test";
            var source = new TestSource {new Setting(key, "Test", "Test", false)};
            var col = new EnvironmentCollection(source);

            try
            {
                Isolate.WhenCalled(() => source.GetEnumerator()).CallOriginal();
                Assert.IsTrue(col.Any(s => s.Name == "TestEnvironment"));
                Isolate.Verify.WasCalledWithAnyArguments(() => source.GetEnumerator());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void ContainFourEnvironmnentsAsSpecifiedInTheTestSource()
        {
            var source = new TestSource
                             {
                                 new Setting("TestMachine1.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.Env2.Test2", "Test", "Conn", false)
                             };
            var col = new EnvironmentCollection(source);

            Assert.AreEqual(4, col.Count);
        }

        [TestMethod]
        public void ContainFourEnvironmnentsAsSpecifiedInTheUnsortedTestSource()
        {
            var source = new TestSource
                             {
                                 new Setting("TestMachine1.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.Env2.Test2", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false)
                             };
            var col = new EnvironmentCollection(source);

            Assert.AreEqual(4, col.Count);
        }

        [TestMethod]
        public void ContainTwoEnvironmnentsForTestMachine1AsSpecifiedInTheUnsortedTestSource()
        {
            var source = new TestSource
                             {
                                 new Setting("TestMachine1.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.Env2.Test2", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false)
                             };
            var col = new EnvironmentCollection(source);

            Assert.AreEqual(2, col.Count(e => e.MachineName == "TestMachine1"));
        }

        [TestMethod]
        public void ContainTwoEnvironmnentsForTestMachine2AsSpecifiedInTheUnsortedTestSource()
        {
            var source = new TestSource
                             {
                                 new Setting("TestMachine1.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.Env2.Test2", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false)
                             };
            var col = new EnvironmentCollection(source);

            Assert.AreEqual(2, col.Count(e => e.MachineName == "TestMachine1"));
        }
    }
}
