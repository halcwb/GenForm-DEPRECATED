using System.Configuration;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Cryptographers;
using Informedica.SecureSettings.Sources;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.SettingsManagement
{
    public class SecureSettingSourceTest
    {
        protected Configuration _configuration;
        protected TestConfigurationFactory _factory;
        protected ConfigurationSettingSource _settingSource;
        protected SecretKeyManager _keyManager;
        protected string _key;
        protected CryptoGraphy _cryptoGraphy;
        protected string _name;
        protected string _encrypted;
        protected SecureSettingSource _secureSettingSource;

        protected void SetupSecureSettingSource()
        {
            _configuration = Isolate.Fake.Instance<Configuration>();
            _factory = new TestConfigurationFactory(_configuration);
            _settingSource = new ConfigurationSettingSource(_factory);

            _keyManager = Isolate.Fake.Instance<SecretKeyManager>();
            _key = "key";
            Isolate.WhenCalled((() => _keyManager.GetKey())).WillReturn(_key);

            _cryptoGraphy = Isolate.Fake.Instance<CryptoGraphy>();
            Isolate.WhenCalled(() => _cryptoGraphy.SetKey(_key)).IgnoreCall();
            _name = "secret";
            _encrypted = "[Secure]=";
            Isolate.WhenCalled((() => _cryptoGraphy.Encrypt(_name))).WillReturn(_encrypted);
            Isolate.WhenCalled((() => _cryptoGraphy.Decrypt(_encrypted))).WillReturn(_name);

            _secureSettingSource = new SecureSettingSource(_settingSource, _keyManager, _cryptoGraphy);
        }
    }
}