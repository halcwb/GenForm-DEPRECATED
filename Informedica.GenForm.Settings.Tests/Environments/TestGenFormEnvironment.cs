using System;
using System.Collections.Generic;
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

            var crypt = new CryptographyAdapter(new SymCryptography());

            var secureSource = new SecureSettingSource(source, keyMan, crypt);

            var envSets = new EnvironmentSettingsCollection("MyMachine", "Test", secureSource);
            envSets.AddSetting("Database", "MyMachine", "Test");
            envSets.AddSetting("LogPath", "MyMachine", "Test");
            envSets.AddSetting("ExportPath", "MyMachine", "Test");

            var env = new Environment("MyMachine", "Test", envSets);
            return new GenFormEnvironment(env);
        }
    }
}