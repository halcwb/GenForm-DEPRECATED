using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.SettingsManagement
{
    [TestClass]
    public class ASecureConfigurationSettingSourceShould: SecureSettingSourceTestFixture
    {
        [TestMethod]
        public void WriteAnEncryptedAppSettingNameToConfiguration()
        {
            SetupSecureSettingSource();

            var setting = new Setting(Name, Name, ConfigurationSettingSource.Types.App.ToString(), true);
            SecureSettingSource.Add(setting);

            Isolate.Verify.WasCalledWithExactArguments(() => Configuration.AppSettings.Settings[Encrypted]);
        }

        [TestMethod]
        public void WriteAnEncryptedConnectionStringToConfiguration()
        {
            SetupSecureSettingSource();

            var setting = new Setting(Name, Name, ConfigurationSettingSource.Types.Conn.ToString(), true);
            SecureSettingSource.Add(setting);

            Isolate.Verify.WasCalledWithExactArguments(() => Configuration.ConnectionStrings.ConnectionStrings[Encrypted]);
        }
    }
}
