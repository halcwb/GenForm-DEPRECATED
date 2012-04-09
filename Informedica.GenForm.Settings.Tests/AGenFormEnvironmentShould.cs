using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests
{
    [TestClass]
    public class AGenFormEnvironmentShould
    {
        private Environment _environment;
        private GenFormEnvironment _genFormEnvironment;
        private EnvironmentSettings _settings;
        private const string EnvironmentName = "TestEnvironment";


        [TestMethod]
        [Isolated]
        public void UseAnEnvironmentToGetTheName()
        {
            SetUpFakeEnvironmentToGetSettings(0);

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
            SetUpFakeEnvironmentToGetSettings(firstSetting);
            
            try
            {
                _genFormEnvironment.GenFormDatabaseConnectionString = "Some string";
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
            SetUpFakeEnvironmentToGetSettings(secondSetting);

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
            SetUpFakeEnvironmentToGetSettings(thirdSetting);

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
            SetUpFakeEnvironmentToGetSettings(secondSetting);

            try
            {
                _genFormEnvironment.GenFormDatabaseConnectionString = "This is a connection string";
                Isolate.Verify.WasCalledWithExactArguments(() => _settings.ElementAt(secondSetting));

                Assert.Fail("The test should fail with a verify exeption");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(TypeMock.VerifyException));
            }
        }

        private void SetUpFakeEnvironmentToGetSettings(int settingIndex)
        {
            _environment = Isolate.Fake.Instance<Environment>();
            Isolate.WhenCalled(() => _environment.Name).WillReturn(EnvironmentName);

            _settings = Isolate.Fake.Instance<EnvironmentSettings>();
            Isolate.WhenCalled(() => _environment.Settings).WillReturn(_settings);
            Isolate.WhenCalled(() => _settings.ElementAt(settingIndex)).WillReturn(Isolate.Fake.Instance<EnvironmentSetting>());

            _genFormEnvironment = new GenFormEnvironment(_environment);
        }
    }
}
