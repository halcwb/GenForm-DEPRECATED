using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.GenForm.Settings.Tests.SettingsManagement;
using Informedica.SecureSettings.Cryptographers;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class EnvironmentSettingsCollectionShould : SecureSettingSourceTestFixture
    {
        private ISetting _setting;
        private ICollection<ISetting> _source;

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionIfNotCreatedWithSecureSource()
        {
            GetIsolatedEnvironmentSettingsCollection();
            try
            {
                new EnvironmentSettingsCollection(null);
                Assert.Fail("Environment setting collection cannot be created without a machinename");
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [Isolated]
        [TestMethod]
        public void NotAcceptTheSameSettingTwice()
        {
            Isolate.CleanUp();
            var envs = GetIsolatedEnvironmentSettingsCollection();
            envs.AddSetting("Test", "Test", "Test", "Test", string.Empty);

            try
            {
                envs.AddSetting("Test", "Test", "Test", "Test", string.Empty);
                Assert.Fail("should not accept the same setting twice");
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException), e.ToString());
            }

        }

        [Isolated]
        [TestMethod]
        public void HaveCountZeroWhenNoEnvironmentSettingsHaveBeenAdded()
        {
            var envs = GetIsolatedEnvironmentSettingsCollection();

            Assert.AreEqual(0, envs.Count());
        }

        [Isolated]
        [TestMethod]
        public void UseSettingSourceToAddAnEnvironmentSetting()
        {
            var envs =GetIsolatedEnvironmentSettingsCollection();
            var fakeSetting = Isolate.Fake.Instance<ISetting>();
            Isolate.WhenCalled(() => fakeSetting.Key).WillReturn("Test");
            try
            {
                Isolate.WhenCalled(() => SecureSettingSource.Add(fakeSetting)).CallOriginal();
                envs.AddSetting("Test", "Test", "Test", "Test", string.Empty);
                Isolate.Verify.WasCalledWithAnyArguments(() => SecureSettingSource.Add(fakeSetting));

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void BeAbleToRemoveAnTestEnvironmentSettingThatWasAdded()
        {
            var envs = GetIsolatedEnvironmentSettingsCollection();

            envs.AddSetting("Test", "Test", "Test", "Test", string.Empty);

            try
            {
                envs.RemoveEnvironmentSetting("Test", "Test");
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void HaveTestEnvironmentSettingWhenTestEnvironmentSettingIsAdded()
        {
            var envs = GetIsolatedEnvironmentSettingsCollection();

            envs.AddSetting("Test", "Test", "Test", "Test", string.Empty);
            Assert.IsNotNull(envs.SingleOrDefault(s => s.Name == "Test"));
        }

        [Isolated]
        [TestMethod]
        public void HaveASettingDatabaseWithValueConnectionStringWhenThisSettingIsAdded()
        {
            var envs = GetIsolatedEnvironmentSettingsCollection();

            envs.AddSetting("MyMachine", "Test", "Database", "Provider", "ConnectionString");
            Assert.AreEqual("ConnectionString", envs.Single(s => s.Name == "Database").ConnectionString);
        }

        [Isolated]
        [TestMethod]
        public void TurnTheSettingNameInMachineEnvironmentNameAndProvider()
        {
            var source = new TestSource();
            var settingname = "TestMachine.TestEnvironment.Test.MyProvider";
            source.Add(SettingFactory.CreateSecureSetting(new ConnectionStringSettings(settingname, string.Empty)));
            var col = new EnvironmentSettingsCollection(source);

            Assert.AreEqual("MyProvider", col.Single(s => s.Name == "Test").Provider);
        }

        [Isolated]
        [TestMethod]
        public void ContainEnvironmentSettingWithNameTestEnvironment()
        {
            var col = GetIsolatedEnvironmentSettingsCollectionWithSettings();
            col.AddSetting("Test", "TestEnvironment", "Test", "Test");

            Assert.IsTrue(col.Any(s => s.Environment == "TestEnvironment"));
        }

        private EnvironmentSettingsCollection GetIsolatedEnvironmentSettingsCollection()
        {
            SecureSettingSource = new TestSource();
            _setting = Isolate.Fake.Instance<ISetting>();
            Isolate.WhenCalled(() => SecureSettingSource.Add(_setting)).CallOriginal();

            var col = new EnvironmentSettingsCollection(SecureSettingSource);

            return col;

        }

        [TestMethod]
        public void NotAcceptTwiceASettingWithTheSameNameForTestMachine1AndEnv1()
        {
            _source = new TestSource
                             {
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test1.Provider", "Test"),
                                 SettingFactory.CreateSecureSetting<ConnectionStringSettings>("TestMachine1.Env1.Test1.Provider", "Other test")                             
                             };

            var col = new EnvironmentSettingsCollection(_source);

            Assert.AreEqual(1, col.Count);
        }


        public EnvironmentSettingsCollection GetIsolatedEnvironmentSettingsCollectionWithSettings()
        {
            SecureSettingSource = new TestSource();
            SecureSettingSource.Add(SettingFactory.CreateSecureSetting<ConnectionStringSettings>("MyMachine.TestEnvironment.Database.SqlProvider", "Connection string to database"));
            //SettingSource.Add(new Setting("MyMachine.TestEnvironment.Database.SqlProvider", "Connection string to database", "Conn", false));
            //SettingSource.Add(new Setting("MyMachine.TestEnvironment.LogPath.FileSystem", "Path to logp", "Conn", false));
            //SettingSource.Add(new Setting("MyMachine.TestEnvironment.ExportPath.FileSystem", "Path to export", "Conn", false));

            //SettingSource.Add(new Setting("OtherMachine.OtherEnvironment.Database.SqlProvider", "Connection string to database", "Conn", false));
            //SettingSource.Add(new Setting("OtherMachine.OtherEnvironment.LogPath.FileSystem", "Path to logp", "Conn", false));
            //SettingSource.Add(new Setting("OtherMachine.OtherEnvironment.ExportPath.FileSystem", "Path to export", "Conn", false));

            KeyManager = Isolate.Fake.Instance<SecretKeyManager>();
            Isolate.WhenCalled(() => KeyManager.GetKey()).WillReturn("secretkey");

            CryptoGraphy = new CryptographyAdapter(new SymCryptography());

            var col = new EnvironmentSettingsCollection(SecureSettingSource);

            return col;
        }

    }
}
