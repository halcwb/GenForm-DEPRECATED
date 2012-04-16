using System.Collections.Generic;
using Informedica.SecureSettings;
using Informedica.SecureSettings.Sources;
using Informedica.SecureSettings.Testing;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    static internal class TestGenFormEnvironment
    {
        public static GenFormEnvironment CreateTestGenFormEnvironment()
        {
            var source = new TestSettingSource(new List<Setting>());
            var secureSource = new SecureSettingSource(source);
            var manager = new SettingsManager(secureSource);
            var settings = new EnvironmentSettings(manager, "Test", "Test");
            
            var env = new Environment("Test", "Test", settings);
            env.Settings.AddSetting(GenFormEnvironment.Settings.Database.ToString(), "Test", "Test");
            env.Settings.AddSetting(GenFormEnvironment.Settings.LogPath.ToString(), "Test", "Test");
            env.Settings.AddSetting(GenFormEnvironment.Settings.ExportPath.ToString(), "Test", "Test");

            var genv = new GenFormEnvironment(env);

            return genv;
        }
    }
}