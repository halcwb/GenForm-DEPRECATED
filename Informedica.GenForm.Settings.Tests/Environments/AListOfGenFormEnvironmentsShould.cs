using System.Collections.Generic;
using System.Configuration;
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
        private ICollection<GenFormEnvironment> _genFormEnvironments;
        private ICollection<EnvironmentSetting> _settings;
        private TestSource _source;
        private EnvironmentCollection _environments;

        [TestInitialize]
        public void SetUpGenFormEnvironments()
        {
            _source = new TestSource();
            _settings = new EnvironmentSettingsCollection(_source);
            _environments = new EnvironmentCollection(_settings);
            _genFormEnvironments = new GenFormEnvironmentCollection(_environments);
        }

        [TestMethod]
        public void UseEnvironmentCollectionToAddANewGenFormEnvironment()
        {
            try
            {
                var genEnv = EnvironmentFactory.CreateGenFormEnvironment("Test", "Test", "Test", "Test");
                Isolate.WhenCalled(() => _environments.Add(null)).IgnoreCall();
                _genFormEnvironments.Add(genEnv);
                Isolate.Verify.WasCalledWithAnyArguments(() => _environments.Add(null));
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void ContainAnEnvironmentWithDatabaseTestEnvironmentTestDatabaseTestDbLogPathTestLpAndExportPathTestEpAfterAdding()
        {
            var genfenv = EnvironmentFactory.CreateGenFormEnvironment("Test", "Test", "Test", "TestDb", "TestLp", "TestEp");
            Assert.AreEqual("TestDb", genfenv.Database);
            Assert.AreEqual("TestLp", genfenv.LogPath);
            Assert.AreEqual("TestEp", genfenv.ExportPath);

            _genFormEnvironments.Add(genfenv);
            genfenv = _genFormEnvironments.Single(e => e.MachineName == genfenv.MachineName && e.Name == genfenv.Name);

            Assert.IsNotNull(genfenv);
            Assert.AreEqual("TestDb", genfenv.Database);
            Assert.AreEqual("TestLp", genfenv.LogPath);
            Assert.AreEqual("TestEp", genfenv.ExportPath);
        }

        [TestMethod]
        public void HaveACountPlusOneAfterAddingANewGenFormEnvironment()
        {
            var count = _genFormEnvironments.Count;

            var genFormEnv = EnvironmentFactory.CreateGenFormEnvironment("Test", "Test", "Test", "test");
            _genFormEnvironments.Add(genFormEnv);

            Assert.AreEqual(count + 1, _genFormEnvironments.Count);
        }

        [TestMethod]
        public void ContainTheNewGenFormEnvironmentAfterAddingIt()
        {
            var genFormEnv = EnvironmentFactory.CreateGenFormEnvironment("Test", "Test", "Test", "Test");
            _genFormEnvironments.Add(genFormEnv);

            Assert.IsTrue(_genFormEnvironments.Any(e => e.MachineName == genFormEnv.MachineName && e.Name == genFormEnv.Name));
        }

        [TestMethod]
        public void OnlyAddANewGenFormEnvironmentWithAName()
        {
            try
            {
                var genv = TestGenFormEnvironmentFactory.CreateTestGenFormEnvironment();
                genv.Database = "Test";
                _genFormEnvironments.Add(genv);

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
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Database.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.LogPath.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.ExportPath.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test4.Provider", "Test")
                             };
            _settings = new EnvironmentSettingsCollection(_source);
            _environments = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(_environments);

            Isolate.WhenCalled(() => _environments.GetEnumerator()).CallOriginal();
            Assert.IsTrue(genFCol.Any());
            Isolate.Verify.WasCalledWithAnyArguments(() => _environments.GetEnumerator());
        }

        [TestMethod]
        [Isolated]
        public void HaveOneGenFormEnvironmentAsSpecifiedInTheTestSource()
        {
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test1", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test1", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv.Database.SqLite", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv.LogPath.FileSystem", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test2", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv.ExportPath.FileSystem", "Test")
                             };
            _settings = new EnvironmentSettingsCollection(_source);
            _environments = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(_environments);

            Assert.IsTrue(genFCol.Any());
        }

        [TestMethod]
        [Isolated]
        public void HaveTwoGenFormEnvironmentAsSpecifiedInTheTestSource()
        {
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test1", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test1", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv1.Database.SqLite", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv1.LogPath.FileSystem", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test2", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv1.ExportPath.FileSystem", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv2.Database.SqLite", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv2.LogPath.FileSystem", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv2.ExportPath.FileSystem", "Test")
                             };
            _settings = new EnvironmentSettingsCollection(_source);
            _environments = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(_environments);

            Assert.IsTrue(genFCol.Count == 2);
        }

        [TestMethod]
        public void HaveTwoGenFormEnvironmentsAsSpecifiedInTheTestSourceWithOtherSettings()
        {
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("[Secure]iCjhnFwaYyeSWXoL5FL7yw==",
                                             "MIwslAvaU0oJDYCnnjkaA58MCH0XsCxesdeCC1ENpFbgUgbs44XbzxjgOLRrtfou+M7jz2bFIw6PyKXNLXWzikIWkps6lrhYuvIffqCrotM="),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("MyMachine.AcceptanceTest1.Database.SqLite",
                                             "MIwslAvaU0oJDYCnnjkaA58MCH0XsCxesdeCC1ENpFbgUgbs44XbzxjgOLRrtfou+M7jz2bFIw6PyKXNLXWzikIWkps6lrhYuvIffqCrotM="),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("MyMachine.AcceptanceTest1.LogPath.FileSystem", "log path"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("MyMachine.AcceptanceTest1.ExportPath.FileSystem", "export path"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("MyMachine.AcceptanceTest2.Database.SqLite",
                                             "MIwslAvaU0oJDYCnnjkaA58MCH0XsCxesdeCC1ENpFbgUgbs44XbzxjgOLRrtfou+M7jz2bFIw6PyKXNLXWzikIWkps6lrhYuvIffqCrotM="),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("MyMachine.AcceptanceTest2.LogPath.FileSystem", "log path"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("MyMachine.AcceptanceTest2.ExportPath.FileSystem", "export path")
                             };

            _settings = new EnvironmentSettingsCollection(_source);
            _environments = new EnvironmentCollection(_settings);

            Assert.AreEqual(2, _environments.Count, "There should be two environments");
            Assert.AreEqual(3, _environments.Single(e => e.Name == "AcceptanceTest1").Settings.Count(), "Environment AcceptanceTest1 should have 3 settings");
            Assert.AreEqual(3, _environments.Single(e => e.Name == "AcceptanceTest2").Settings.Count(), "Environment AcceptanceTest2 should have 3 settings");

            var genfCol = new GenFormEnvironmentCollection(_environments);

            Assert.AreEqual(2, genfCol.Count, "There should be only two GenForm environments");

        }

        [TestMethod]
        [Isolated]
        public void HaveOneGenFormEnvironmentWhenOtherEnvironmentTypesWithSameNameInTestSource()
        {
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.GenFormEnv.Test1", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test1", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv.Database.SqLite", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv.LogPath.FileSystem", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test2", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine2.GenFormEnv.ExportPath.FileSystem", "Test")
                             };
            _settings = new EnvironmentSettingsCollection(_source);
            _environments = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(_environments);

            Assert.IsTrue(genFCol.Any());
        }
    }
}
