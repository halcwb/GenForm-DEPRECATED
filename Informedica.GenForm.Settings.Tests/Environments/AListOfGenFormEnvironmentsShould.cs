using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.GenForm.Settings.Tests.SettingsManagement;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class AListOfGenFormEnvironmentsShould : SecureSettingSourceTestFixture
    {
        private ICollection<GenFormEnvironment> _environments;

        [TestInitialize]
        public void SetUpGenFormEnvironments()
        {
            _environments = new GenFormEnvironmentCollection();
        }

        [TestMethod]
        public void OnlyAddANewGenFormEnvironmentWithAName()
        {
            try
            {
                var genv = TestGenFormEnvironmentFactory.CreateTestGenFormEnvironment();
                genv.Database = "Test";
                _environments.Add(genv);

            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        [Isolated]
        public void UseAEnvironmentsCollectionToGetTheGenFormEnvironments()
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

            var genFCol = new GenFormEnvironmentCollection(col);

            Isolate.WhenCalled(() => col.GetEnumerator()).CallOriginal();
            Assert.IsFalse(genFCol.Any());
            Isolate.Verify.WasCalledWithAnyArguments(() => col.GetEnumerator());
        }

        [TestMethod]
        [Isolated]
        public void HaveOneGenFormEnvironmentAsSpecifiedInTheTestSource()
        {
            var source = new TestSource
                             {
                                 new Setting("TestMachine1.Env1.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.GenFormEnv.Database.SqLite", "Test", "Conn", false),
                                 new Setting("TestMachine2.GenFormEnv.LogPath.FileSystem", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false),
                                 new Setting("TestMachine2.GenFormEnv.ExportPath.FileSystem", "Test", "Conn", false)
                             };
            var col = new EnvironmentCollection(source);

            var genFCol = new GenFormEnvironmentCollection(col);

            Assert.IsTrue(genFCol.Any());
        }

        [TestMethod]
        [Isolated]
        public void HaveOneGenFormEnvironmentWhenOtherEnvironmentTypesWithSameNameInTestSource()
        {
            var source = new TestSource
                             {
                                 new Setting("TestMachine1.GenFormEnv.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
                                 new Setting("TestMachine2.GenFormEnv.Database.SqLite", "Test", "Conn", false),
                                 new Setting("TestMachine2.GenFormEnv.LogPath.FileSystem", "Test", "Conn", false),
                                 new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false),
                                 new Setting("TestMachine2.GenFormEnv.ExportPath.FileSystem", "Test", "Conn", false)
                             };
            var col = new EnvironmentCollection(source);

            var genFCol = new GenFormEnvironmentCollection(col);

            Assert.IsTrue(genFCol.Any());
        }
    }
}
