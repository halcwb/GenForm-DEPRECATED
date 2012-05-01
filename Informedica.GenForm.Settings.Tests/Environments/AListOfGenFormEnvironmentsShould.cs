using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.GenForm.Settings.Tests.SettingsManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class AListOfGenFormEnvironmentsShould : SecureSettingSourceTestFixture
    {
        private ICollection<GenFormEnvironment> _environments;
        private ICollection<EnvironmentSetting> _settings;

        [TestInitialize]
        public void SetUpGenFormEnvironments()
        {
            _settings = new Collection<EnvironmentSetting>();
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
            //_source = new TestSource
            //              {
            //                  new Setting("TestMachine1.Env1.Test1", "Test", "Conn", false),
            //                  new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
            //                  new Setting("TestMachine2.Env1.Test1", "Test", "Conn", false),
            //                  new Setting("TestMachine2.Env2.Test2", "Test", "Conn", false),
            //                  new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false)
            //              };
            var col = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(col);

            Isolate.WhenCalled(() => col.GetEnumerator()).CallOriginal();
            Assert.IsFalse(genFCol.Any());
            Isolate.Verify.WasCalledWithAnyArguments(() => col.GetEnumerator());
        }

        [TestMethod]
        [Isolated]
        public void HaveOneGenFormEnvironmentAsSpecifiedInTheTestSource()
        {
            //_source = new TestSource
            //                 {
            //                     new Setting("TestMachine1.Env1.Test1", "Test", "Conn", false),
            //                     new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv.Database.SqLite", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv.LogPath.FileSystem", "Test", "Conn", false),
            //                     new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv.ExportPath.FileSystem", "Test", "Conn", false)
            //                 };
            var col = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(col);

            Assert.IsTrue(genFCol.Any());
        }

        [TestMethod]
        [Isolated]
        public void HaveTwoGenFormEnvironmentAsSpecifiedInTheTestSource()
        {
            //_source = new TestSource
            //                 {
            //                     new Setting("TestMachine1.Env1.Test1", "Test", "Conn", false),
            //                     new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv1.Database.SqLite", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv1.LogPath.FileSystem", "Test", "Conn", false),
            //                     new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv1.ExportPath.FileSystem", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv2.Database.SqLite", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv2.LogPath.FileSystem", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv2.ExportPath.FileSystem", "Test", "Conn", false)
            //                 };
            var col = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(col);

            Assert.IsTrue(genFCol.Count == 2);
        }

        [TestMethod]
        public void HaveTwoGenFormEnvironmentsAsSpecifiedInTheTestSourceWithOtherSettings()
        {
            //_source = new TestSource
            //                 {
            //                     new Setting("[Secure]iCjhnFwaYyeSWXoL5FL7yw==",
            //                                 "MIwslAvaU0oJDYCnnjkaA58MCH0XsCxesdeCC1ENpFbgUgbs44XbzxjgOLRrtfou+M7jz2bFIw6PyKXNLXWzikIWkps6lrhYuvIffqCrotM=",
            //                                 "Conn", false),
            //                     new Setting("MyMachine.AcceptanceTest1.Database.SqLite",
            //                                 "MIwslAvaU0oJDYCnnjkaA58MCH0XsCxesdeCC1ENpFbgUgbs44XbzxjgOLRrtfou+M7jz2bFIw6PyKXNLXWzikIWkps6lrhYuvIffqCrotM=",
            //                                 "Conn", false),
            //                     new Setting("MyMachine.AcceptanceTest1.LogPath.FileSystem", "log path", "Conn", false),
            //                     new Setting("MyMachine.AcceptanceTest1.ExportPath.FileSystem", "export path", "Conn",
            //                                 false),
            //                     new Setting("MyMachine.AcceptanceTest2.Database.SqLite",
            //                                 "MIwslAvaU0oJDYCnnjkaA58MCH0XsCxesdeCC1ENpFbgUgbs44XbzxjgOLRrtfou+M7jz2bFIw6PyKXNLXWzikIWkps6lrhYuvIffqCrotM=",
            //                                 "Conn", false),
            //                     new Setting("MyMachine.AcceptanceTest2.LogPath.FileSystem", "log path", "Conn", false),
            //                     new Setting("MyMachine.AcceptanceTest2.ExportPath.FileSystem", "export path", "Conn",
            //                                 false)
            //                 };

            //var secure = new SecureSettingSource(source, new SecretKeyManager(), CryptographyFactory.GetCryptography());
            //Assert.AreEqual(source.Count, secure.Count, "Source and SecureSource should have equal setting count");

            //var col = new EnvironmentCollection(secure);
            //Assert.AreEqual(2, col.Count, "There should be two environments");
            //Assert.AreEqual(3, col.Single(e => e.Name == "AcceptanceTest1").Settings.Count(), "Environment AcceptanceTest1 should have 3 settings");
            //Assert.AreEqual(3, col.Single(e => e.Name == "AcceptanceTest2").Settings.Count(), "Environment AcceptanceTest2 should have 3 settings");

            //var genfCol = new GenFormEnvironmentCollection(col);

            //Assert.AreEqual(2, genfCol.Count, "There should be only two GenForm environments");

        }

        [TestMethod]
        [Isolated]
        public void HaveOneGenFormEnvironmentWhenOtherEnvironmentTypesWithSameNameInTestSource()
        {
            //_source = new TestSource
            //                 {
            //                     new Setting("TestMachine1.GenFormEnv.Test1", "Test", "Conn", false),
            //                     new Setting("TestMachine1.Env2.Test1", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv.Database.SqLite", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv.LogPath.FileSystem", "Test", "Conn", false),
            //                     new Setting("TestMachine1.Env1.Test2", "Test", "Conn", false),
            //                     new Setting("TestMachine2.GenFormEnv.ExportPath.FileSystem", "Test", "Conn", false)
            //                 };
            var col = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(col);

            Assert.IsTrue(genFCol.Any());
        }
    }
}
