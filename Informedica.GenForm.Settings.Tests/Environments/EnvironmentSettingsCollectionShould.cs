using System;
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
        private Setting _setting;

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionIfNotCreatedWithEnvironmentNameName()
        {
            GetIsolatedEnvironmentSettingsCollection();
            try
            {
                new EnvironmentSettingsCollection("Test", string.Empty, SecureSettingSource);
                Assert.Fail("Environment setting collection cannot be created without a machinename");
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionIfNotCreatedWithSecureSource()
        {
            GetIsolatedEnvironmentSettingsCollection();
            try
            {
                new EnvironmentSettingsCollection("Test", "Test", null);
                Assert.Fail("Environment setting collection cannot be created without a machinename");
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionIfNotCreatedWithMachineName()
        {
            GetIsolatedEnvironmentSettingsCollection();
            try
            {
                new EnvironmentSettingsCollection(string.Empty, "Test", SecureSettingSource);
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
            envs.AddSetting("Test", "Test");

            try
            {
                envs.AddSetting("Test", "Test");
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
            var fakeSetting = Isolate.Fake.Instance<Setting>();

            try
            {
                Isolate.WhenCalled(() => SecureSettingSource.Add(fakeSetting)).CallOriginal();
                envs.AddSetting("Test", "Test");
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

            envs.AddSetting("Test", "Test");

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

            envs.AddSetting("Test", "Test");
            Assert.IsNotNull(envs.SingleOrDefault(s => s.Name == "Test"));
        }

        [Isolated]
        [TestMethod]
        public void HaveASettingDatabaseWithValueConnectionStringWhenThisSettingIsAdded()
        {
            var envs = GetIsolatedEnvironmentSettingsCollection();

            envs.AddSetting("Database", "Provider", "ConnectionString");
            Assert.AreEqual("ConnectionString", envs.Single(s => s.Name == "Database").ConnectionString);
        }

        [Isolated]
        [TestMethod]
        public void TurnTheSettingNameInMachineEnvironmentNameAndProvider()
        {
            var settingname = "TestMachine.TestEnvironment.Test.MyProvider";
            var col = GetIsolatedEnvironmentSettingsCollection();
            SettingSource.Add(new Setting(settingname, "Connection string", "Conn", false));

            Assert.AreEqual("MyProvider", col.Single(s => s.Name == "Test").Provider);
        }

        [Isolated]
        [TestMethod]
        public void ContainEnvironmentSettingWithNameTestEnvironment()
        {
            var col = GetIsolatedEnvironmentSettingsCollectionWithSettings();
            Assert.IsTrue(col.Any(s => s.Environment == "TestEnvironment"));
        }

        private EnvironmentSettingsCollection GetIsolatedEnvironmentSettingsCollection()
        {
            SettingSource = new TestSource();
            _setting = Isolate.Fake.Instance<Setting>();
            Isolate.WhenCalled(() => SettingSource.Add(_setting)).CallOriginal();

            KeyManager = Isolate.Fake.Instance<SecretKeyManager>();
            Isolate.WhenCalled(() => KeyManager.GetKey()).WillReturn("secretkey");

            CryptoGraphy = new CryptographyAdapter(new SymCryptography());
            SecureSettingSource = new SecureSettingSource(SettingSource, KeyManager, CryptoGraphy);

            var col = new EnvironmentSettingsCollection("TestMachine", "TestEnvironment", SecureSettingSource);

            return col;

        }

        public EnvironmentSettingsCollection GetIsolatedEnvironmentSettingsCollectionWithSettings()
        {
            SettingSource = new TestSource();
            SettingSource.Add(new Setting("MyMachine.TestEnvironment.Database.SqlProvider", "Connection string to database", "Conn", false));
            SettingSource.Add(new Setting("MyMachine.TestEnvironment.LogPath.FileSystem", "Path to logp", "Conn", false));
            SettingSource.Add(new Setting("MyMachine.TestEnvironment.ExportPath.FileSystem", "Path to export", "Conn", false));

            SettingSource.Add(new Setting("OtherMachine.OtherEnvironment.Database.SqlProvider", "Connection string to database", "Conn", false));
            SettingSource.Add(new Setting("OtherMachine.OtherEnvironment.LogPath.FileSystem", "Path to logp", "Conn", false));
            SettingSource.Add(new Setting("OtherMachine.OtherEnvironment.ExportPath.FileSystem", "Path to export", "Conn", false));

            KeyManager = Isolate.Fake.Instance<SecretKeyManager>();
            Isolate.WhenCalled(() => KeyManager.GetKey()).WillReturn("secretkey");

            CryptoGraphy = new CryptographyAdapter(new SymCryptography());
            SecureSettingSource = new SecureSettingSource(SettingSource, KeyManager, CryptoGraphy);

            var col = new EnvironmentSettingsCollection("TestMachine", "TestEnvironment", SecureSettingSource);

            return col;
        }

    }
}
