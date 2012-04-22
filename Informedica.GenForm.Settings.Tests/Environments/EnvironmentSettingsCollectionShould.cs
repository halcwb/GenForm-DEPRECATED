using System;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Cryptographers;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class EnvironmentSettingsCollectionShould
    {
        private SecureSettingSource _secureSource;
        private SettingSource _source;
        private SecretKeyManager _keyMan;
        private CryptoGraphy _crypt;
        private Setting _setting;

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionIfNotCreatedWithEnvironmentNameName()
        {
            GetIsolatedEnvironmentSettingsCollection();
            try
            {
                new EnvironmentSettingsCollection("Test", string.Empty, _secureSource);
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
                new EnvironmentSettingsCollection(string.Empty, "Test", _secureSource);
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
            envs.AddSetting("Test");

            try
            {
                envs.AddSetting("Test");
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
                envs.AddSetting("Test");
                Isolate.Verify.WasCalledWithAnyArguments(() => _source.WriteSetting(fakeSetting));

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

            envs.AddSetting("Test");

            try
            {
                envs.RemoveEnvironmentSetting("Test");
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

            envs.AddSetting("Test");
            Assert.IsNotNull(envs.SingleOrDefault(s => s.Name == "Test"));
        }

        [Isolated]
        [TestMethod]
        public void HaveASettingDatabaseWithValueConnectionStringWhenThisSettingIsAdded()
        {
            var envs = GetIsolatedEnvironmentSettingsCollection();

            envs.AddSetting("Database", "ConnectionString");
            Assert.AreEqual("ConnectionString", envs.Single(s => s.Name == "Database").ConnectionString);
        }

        private EnvironmentSettingsCollection GetIsolatedEnvironmentSettingsCollection()
        {
            _source = new TestSource();
            _setting = Isolate.Fake.Instance<Setting>();
            Isolate.WhenCalled(() => _source.WriteSetting(_setting)).CallOriginal();

            _keyMan = Isolate.Fake.Instance<SecretKeyManager>();
            Isolate.WhenCalled(() => _keyMan.GetKey()).WillReturn("secretkey");

            _crypt = new CryptographyAdapter(new SymCryptography());
            _secureSource = new SecureSettingSource(_source, _keyMan, _crypt);

            var col = new EnvironmentSettingsCollection("TestMachine", "TestEnvironment", _secureSource);

            return col;
        }

    }
}
