using System.Threading;
using Informedica.SecureSettings;
using StructureMap;

namespace Informedica.GenForm.Settings
{
    public class SettingsManager
    {
        static SettingsManager _instance;
        static readonly object LockThis = new object();

        private readonly SecureSettingsManager _secure;

        public SettingsManager(SecureSettingsManager secureSettingsManager)
        {
            _secure = secureSettingsManager;
        }

        #region Singleton

        public static SettingsManager Instance
        {
            get
            {
                lock (LockThis)
                {
                    if (_instance == null)
                    {
                        var instance = new SettingsManager(GetSecureSettingsManager());
                        Thread.MemoryBarrier();
                        _instance = instance;
                    }
                    return _instance;
                }
            }
        }

        private static SecureSettingsManager GetSecureSettingsManager()
        {
            return new SecureSettingsManager(GetSettingSource());
        }

        private static ISettingSource GetSettingSource()
        {
           return new SettingSource();
        }

        #endregion

        public string ReadSecureSetting(string name)
        {
            return GetSettingReader().ReadSetting(name);
        }

        private SettingReader GetSettingReader()
        {
            return ObjectFactory.GetInstance<SettingReader>();
        }

        public void WriteSecureSetting(string key, string value)
        {
            GetSettingWriter().WriteSetting(key, value);
        }

        private ConfigurationManagerSettingWriter GetSettingWriter()
        {
            return new ConfigurationManagerSettingWriter();
        }

        public string GetExporthPath()
        {
            return Properties.Settings.Default.ExportPath;
        }

        public string GetLogPath()
        {
            return Properties.Settings.Default.LogPath;
        }

        public string GetConnectionString(string environment)
        {
            return _secure.GetConnectionString(environment);
        }

        public void AddConnectionString(string name, string connectionString)
        {
            _secure.SetConnectionString(name, connectionString);
        }
    }
}
