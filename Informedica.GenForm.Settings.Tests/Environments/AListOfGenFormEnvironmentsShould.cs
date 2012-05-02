using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ICollection<GenFormEnvironment> _environments;
        private ICollection<EnvironmentSetting> _settings;
        private TestSource _source;

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
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Database.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.LogPath.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.ExportPath.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env2.Test4.Provider", "Test")
                             };
            _settings = new EnvironmentSettingsCollection(_source);
            var col = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(col);

            Isolate.WhenCalled(() => col.GetEnumerator()).CallOriginal();
            Assert.IsTrue(genFCol.Any());
            Isolate.Verify.WasCalledWithAnyArguments(() => col.GetEnumerator());
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
            var col = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(col);

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
            var col = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(col);

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
            var col = new EnvironmentCollection(_settings);

            Assert.AreEqual(2, col.Count, "There should be two environments");
            Assert.AreEqual(3, col.Single(e => e.Name == "AcceptanceTest1").Settings.Count(), "Environment AcceptanceTest1 should have 3 settings");
            Assert.AreEqual(3, col.Single(e => e.Name == "AcceptanceTest2").Settings.Count(), "Environment AcceptanceTest2 should have 3 settings");

            var genfCol = new GenFormEnvironmentCollection(col);

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
            var col = new EnvironmentCollection(_settings);

            var genFCol = new GenFormEnvironmentCollection(col);

            Assert.IsTrue(genFCol.Any());
        }
    }
}
