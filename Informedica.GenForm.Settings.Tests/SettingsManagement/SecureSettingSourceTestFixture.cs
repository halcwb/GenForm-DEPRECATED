using System.Collections.Generic;
using System.Configuration;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.SecureSettings.Cryptographers;
using Informedica.SecureSettings.Sources;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.SettingsManagement
{
    public class SecureSettingSourceTestFixture
    {
        protected Configuration Configuration;
        protected TestConfigurationFactory Factory;
        protected ICollection<Setting> SettingSource;
        protected SecretKeyManager KeyManager;
        protected string Key;
        protected CryptoGraphy CryptoGraphy;
        protected string Name;
        protected string Encrypted;
        protected ICollection<Setting> SecureSettingSource;

        protected void SetupSecureSettingSource()
        {
            Configuration = Isolate.Fake.Instance<Configuration>();
            Factory = new TestConfigurationFactory(Configuration);
            SettingSource = new ConfigurationSettingSource(Factory);

            KeyManager = Isolate.Fake.Instance<SecretKeyManager>();
            Key = "key";
            Isolate.WhenCalled((() => KeyManager.GetKey())).WillReturn(Key);

            CryptoGraphy = Isolate.Fake.Instance<CryptoGraphy>();
            Isolate.WhenCalled(() => CryptoGraphy.SetKey(Key)).IgnoreCall();
            Name = "secret";
            Encrypted = "[Secure]=";
            Isolate.WhenCalled((() => CryptoGraphy.Encrypt(Name))).WillReturn(Encrypted);
            Isolate.WhenCalled((() => CryptoGraphy.Decrypt(Encrypted))).WillReturn(Name);

            SecureSettingSource = new SecureSettingSource(SettingSource, KeyManager, CryptoGraphy);
        }
    }
}