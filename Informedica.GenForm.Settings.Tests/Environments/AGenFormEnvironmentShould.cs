using System;
using System.Collections.Generic;
using Informedica.GenForm.Settings.ConfigurationSettings;
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
        private ISetting _setting;
        private SecretKeyManager _keyMan;
        private const string MachineName = "TestMachine";
        private const string EnvironmentName = "TestEnvironment";

        private EnvironmentSettingsCollection GetIsolatedEnvironmentSettingsCollection()
        {
            _source = new TestSource();
            _setting = Isolate.Fake.Instance<ISetting>();
            Isolate.WhenCalled(() => _source.Add(_setting)).CallOriginal();

            _keyMan = Isolate.Fake.Instance<SecretKeyManager>();
            Isolate.WhenCalled(() => _keyMan.GetKey()).WillReturn("secretkey");

            var col = new EnvironmentSettingsCollection(_source);

            return col;
        }

        private void GetEnvironment(string database, string logpath, string exportpath, string connectionstring, string provider)
        {
            _settings = GetIsolatedEnvironmentSettingsCollection();
            if (!string.IsNullOrWhiteSpace(database)) _settings.AddSetting(MachineName, EnvironmentName, database, provider, connectionstring);
            if (!string.IsNullOrWhiteSpace(logpath)) _settings.AddSetting(MachineName, EnvironmentName, logpath, provider);
            if (!string.IsNullOrWhiteSpace(exportpath)) _settings.AddSetting(MachineName, EnvironmentName, exportpath, provider);

            _environment = new Environment(MachineName, EnvironmentName, _settings);
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
            var genv = TestGenFormEnvironmentFactory.CreateTestGenFormEnvironment();
            genv.Database = "Test";

            Assert.AreEqual("Test", genv.Database);
        }

    }
}
