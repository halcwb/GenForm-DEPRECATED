using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Settings.ConfigurationSettings;
using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Cryptographers;
using Informedica.SecureSettings.Sources;
using Informedica.SecureSettings.Testing;
using TypeMock.ArrangeActAssert;
using Environment = Informedica.GenForm.Settings.Environments.Environment;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    static internal class TestGenFormEnvironment
    {
        [Obsolete]
        public static GenFormEnvironment CreateTestGenFormEnvironment_Old()
        {
            var source = new TestSettingSource(new List<Setting>());
            var secureSource = new SecureSettingSource(source);
            var manager = new SettingsManager(secureSource);
            var settings = new EnvironmentSettingsCollection(manager, "Test", "Test");
            
            var env = new Environment("Test", "Test", settings);
            env.Settings.AddSetting_Old(GenFormEnvironment.Settings.Database.ToString(), "Test", "Test");
            env.Settings.AddSetting_Old(GenFormEnvironment.Settings.LogPath.ToString(), "Test", "Test");
            env.Settings.AddSetting_Old(GenFormEnvironment.Settings.ExportPath.ToString(), "Test", "Test");

            var genv = new GenFormEnvironment(env);

            return genv;
        }

        public static GenFormEnvironment CreateTestGenFormEnvironment()
        {
            var source = new TestSource();
            
            var keyMan = Isolate.Fake.Instance<SecretKeyManager>();
            Isolate.WhenCalled(() => keyMan.GetKey()).WillReturn("secret");
            Isolate.WhenCalled(() => keyMan.SetKey("secret")).IgnoreCall();

            var crypt = Isolate.Fake.Instance<CryptoGraphy>();
            Isolate.WhenCalled(() => crypt.Decrypt("[Secure]")).WillReturn("");
            Isolate.WhenCalled(() => crypt.Encrypt("")).WillReturn("[Secure]");
            Isolate.WhenCalled(() => crypt.Decrypt("[Secure]Database.MyMachine.Test")).WillReturn("Database.MyMachine.Test");
            Isolate.WhenCalled(() => crypt.Encrypt("Database.MyMachine.Test")).WillReturn("[Secure]Database.MyMachine.Test");
            Isolate.WhenCalled(() => crypt.Decrypt("[Secure]LogPath.MyMachine.Test")).WillReturn("LogPath.MyMachine.Test");
            Isolate.WhenCalled(() => crypt.Encrypt("LogPath.MyMachine.Test")).WillReturn("[Secure]LogPath.MyMachine.Test");
            Isolate.WhenCalled(() => crypt.Decrypt("[Secure]ExportPath.MyMachine.Test")).WillReturn("ExportPath.MyMachine.Test");
            Isolate.WhenCalled(() => crypt.Encrypt("ExportPath.MyMachine.Test")).WillReturn("[Secure]ExportPath.MyMachine.Test");

            var secureSource = new SecureSettingSource(source, keyMan, crypt);

            var envSets = new EnvironmentSettingsCollection("MyMachine", "Test", secureSource);
            envSets.AddSetting("Database", "MyMachine", "Test");
            envSets.AddSetting("LogPath", "MyMachine", "Test");
            envSets.AddSetting("ExportPath", "MyMachine", "Test");

            var env = new Environment("MyMachine", "Test", envSets);
            return new GenFormEnvironment(env);
        }
    }

    public class TestSource: SettingSource
    {
        private IList<Setting> _settings;

        #region Overrides of SettingSource

        protected override Enum SettingTypeToEnum(Setting setting)
        {
            return ConfigurationSettingSource.Types.Conn;
        }

        protected override void RegisterReaders()
        {
            Readers.Add(ConfigurationSettingSource.Types.Conn, ReadConnSetting);
        }

        private Setting ReadConnSetting(string arg)
        {
            return Settings.SingleOrDefault(S => S.Name == arg);
        }

        protected override void RegisterWriters()
        {
            Writers.Add(ConfigurationSettingSource.Types.Conn, WriteConnSetting);
        }

        private void WriteConnSetting(Setting obj)
        {
            if (!Settings.Any(s => s.Name == obj.Name)) _settings.Add(obj);
            else Settings.Single(s => s.Name == obj.Name).Value = obj.Value;
        }

        protected override void RegisterRemovers()
        {
            Removers.Add(ConfigurationSettingSource.Types.Conn, RemoveConnSetting);
        }

        private void RemoveConnSetting(Setting obj)
        {
            if (Settings.Any(s => s.Name == obj.Name)) _settings.Remove(Settings.Single(s => s.Name == obj.Name));
        }

        protected override IEnumerable<Setting> Settings
        {
            get { return _settings ?? (_settings =  new List<Setting>()); }
        }

        public override void Save()
        {
        }

        #endregion
    }
}