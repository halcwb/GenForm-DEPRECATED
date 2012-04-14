using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class AGenFormEnvironmentShould
    {
        private Environment _environment;
        private GenFormEnvironment _genFormEnvironment;
        private EnvironmentSettings _settings;
        private const string EnvironmentName = "TestEnvironment";
        private const int SettingCount = 3;

        [TestMethod]
        [Isolated]
        public void UseAnEnvironmentWithThreeSettings()
        {
            try
            {
                var wrongSettingCount = 0;
                SetUpFakeEnvironmentToGetSettings(wrongSettingCount);
                Assert.Fail("GenForm environment was instantiated with less than 3 settings");
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
            SetUpFakeEnvironmentToGetSettings(SettingCount);

            Assert.AreEqual(EnvironmentName, _genFormEnvironment.Name);

            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => _environment.Name);
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }


        [TestMethod]
        [Isolated]
        public void UseTheFirstSettingInEnvironmentSettingsToSetAndGetTheDatabaseConnectionString()
        {
            const int firstSetting = 0;
            SetUpFakeEnvironmentToGetSettings(SettingCount);
            
            try
            {
                _genFormEnvironment.Database = "Some string";
                Isolate.Verify.WasCalledWithExactArguments(() => _settings.ElementAt(firstSetting));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

 
        [TestMethod]
        [Isolated]
        public void UseTheSecondSettingInEnvironmentToSetAndGetTheLogPath()
        {
            const int secondSetting = 1;
            SetUpFakeEnvironmentToGetSettings(SettingCount);

            try
            {
                const string logpath = "Some logpath";
                _genFormEnvironment.LogPath = logpath;
                Isolate.Verify.WasCalledWithExactArguments(() => _settings.ElementAt(secondSetting));

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }

        }

        [TestMethod]
        [Isolated]
        public void UseTheThirdSettingInEnvironmentToSetAndGetTheExportPath()
        {
            const int thirdSetting = 2;
            SetUpFakeEnvironmentToGetSettings(SettingCount);

            try
            {
                _genFormEnvironment.ExportPath = "An export path";
                Isolate.Verify.WasCalledWithExactArguments(() => _settings.ElementAt(thirdSetting));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        [Isolated]
        public void NotUseTheSecondSettingInEnvironmentToSetAndGetDatabaseConnectionString()
        {
            const int secondSetting = 1;
            SetUpFakeEnvironmentToGetSettings(SettingCount);

            try
            {
                _genFormEnvironment.Database = "This is a connection string";
                Isolate.Verify.WasCalledWithExactArguments(() => _settings.ElementAt(secondSetting));

                Assert.Fail("The test should fail with a verify exeption");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(TypeMock.VerifyException));
            }
        }

        [TestMethod]
        public void HaveDatabaseTestWhenSetToTest()
        {
            var genv = TestGenFormEnvironment.CreateTestGenFormEnvironment();
            genv.Database = "Test";

            Assert.AreEqual("Test", genv.Database);
        }

        private void SetUpFakeEnvironmentToGetSettings(int count)
        {
            _environment = Isolate.Fake.Instance<Environment>();
            Isolate.WhenCalled(() => _environment.Name).WillReturn(EnvironmentName);

            _settings = Isolate.Fake.Instance<EnvironmentSettings>();
            Isolate.WhenCalled(() => _settings.Count()).WillReturn(count);
            Isolate.WhenCalled(() => _environment.Settings).WillReturn(_settings);
            for (var i = 0; i < count; i++)
            {
                var index = i;
                Isolate.WhenCalled(() => _settings.ElementAt(index)).WillReturn(
                    Isolate.Fake.Instance<EnvironmentSetting>());
            }

            _genFormEnvironment = new GenFormEnvironment(_environment);
        }
    }
}
