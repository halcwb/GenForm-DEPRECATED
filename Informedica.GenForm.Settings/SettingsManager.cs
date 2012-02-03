using System.Threading;
using Informedica.SecureSettings;

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
            return _secure.GetSecureSetting(name);
        }

        public void WriteSecureSetting(string key, string value)
        {
            _secure.WriteSecureSetting(key, value);
        }

        public string GetExporthPath()
        {
            return _secure.GetSecureSetting("exppath");
        }

        public string GetLogPath()
        {
            return _secure.GetSecureSetting("logpath");
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
