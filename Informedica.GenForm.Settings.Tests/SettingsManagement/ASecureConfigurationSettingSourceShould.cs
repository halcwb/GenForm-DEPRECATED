using System;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Sources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.SettingsManagement
{
    [TestClass]
    public class ASecureConfigurationSettingSourceShould: SecureSettingSourceTest
    {
        [TestMethod]
        public void WriteAnEncryptedAppSettingNameToConfiguration()
        {
            SetupSecureSettingSource();

            var setting = new Setting(_name, _name, ConfigurationSettingSource.Types.App.ToString(), true);
            _secureSettingSource.WriteSecure(setting);

            Isolate.Verify.WasCalledWithExactArguments(() => _configuration.AppSettings.Settings[_encrypted]);
        }

        [TestMethod]
        public void WriteAnEncryptedConnectionStringToConfiguration()
        {
            SetupSecureSettingSource();

            var setting = new Setting(_name, _name, ConfigurationSettingSource.Types.Conn.ToString(), true);
            _secureSettingSource.WriteSecure(setting);

            Isolate.Verify.WasCalledWithExactArguments(() => _configuration.ConnectionStrings.ConnectionStrings[_encrypted]);
        }
    }
}
