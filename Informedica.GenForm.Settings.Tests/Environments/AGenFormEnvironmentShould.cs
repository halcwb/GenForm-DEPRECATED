using System;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Cryptographers;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using Environment = Informedica.GenForm.Settings.Environments.Environment;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class AGenFormEnvironmentShould
    {
        private Environment _environment;
        private GenFormEnvironment _genFormEnvironment;
        private EnvironmentSettingsCollection _settings;
        private SettingSource _source;
        private Setting _setting;
        private SecretKeyManager _keyMan;
        private CryptoGraphy _crypt;
        private SecureSettingSource _secureSource;
        private const string EnvironmentName = "Test";

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

        private void GetEnvironment(string database, string logpath, string exportpath, string connectionstring, string provider)
        {
            _settings = GetIsolatedEnvironmentSettingsCollection();
            if (!string.IsNullOrWhiteSpace(database)) _settings.AddSetting(database, provider, connectionstring);
            if (!string.IsNullOrWhiteSpace(logpath)) _settings.AddSetting(logpath, provider);
            if (!string.IsNullOrWhiteSpace(exportpath)) _settings.AddSetting(exportpath, provider);

            _environment = new Environment("Test", "Test", _settings);
            _genFormEnvironment = new GenFormEnvironment(_environment);
        }

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionWhenCreatedWithoutADatabaseConnectionString()
        {
            try
            {
                GetEnvironment("Database", "LogPath", "ExportPath", string.Empty, "provider");
                Assert.Fail("GenFormEnvironment cannot be created without a database connection string");

            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }


        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionWhenCreatedWithoutADatabaseProvider()
        {
            try
            {
                GetEnvironment("Database", "LogPath", "ExportPath", "Some connection string", string.Empty);
                Assert.Fail("GenFormEnvironment cannot be created without a database connection string");

            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionWhenCreatedWithoutADatabaseSetting()
        {
            try
            {
                GetEnvironment(string.Empty, "LogPath", "ExportPath", "Some connection string", "provider");
                Assert.Fail("GenFormEnvironment cannot be created without a database connection string");

            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionWhenCreatedWithoutALogPathSetting()
        {
            try
            {
                GetEnvironment("Database", string.Empty, "ExportPath", "Some connection string", "provider");
                Assert.Fail("GenFormEnvironment cannot be created without a database connection string");

            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [Isolated]
        [TestMethod]
        public void ThrowAnExceptionWhenCreatedWithoutAnExportPathSetting()
        {
            try
            {
                GetEnvironment("Database", "LogPath", string.Empty, "Some connection string", "provider");
                Assert.Fail("GenFormEnvironment cannot be created without a database connection string");

            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [TestMethod]
        [Isolated]
        public void UseAnEnvironmentToGetTheName()
        {
            GetEnvironment("Database", "LogPath", "ExportPath", "Some connection string", "provider");

            Isolate.WhenCalled(() => _environment.Name).CallOriginal();
            try
            {
                Assert.AreEqual(EnvironmentName, _genFormEnvironment.Name);
                Isolate.Verify.WasCalledWithAnyArguments(() => _environment.Name);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void HaveDatabaseTestWhenSetToTest()
        {
            var genv = TestGenFormEnvironment.CreateTestGenFormEnvironment();
            genv.Database = "Test";

            Assert.AreEqual("Test", genv.Database);
        }

    }
}
