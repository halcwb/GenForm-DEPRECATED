using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Informedica.SecureSettings.Sources;

namespace Informedica.GenForm.Settings
{
    public class SettingsManager
    {
        static SettingsManager _instance;
        static readonly object LockThis = new object();

        private readonly ISettingSource _secure;

        public SettingsManager(ISettingSource secureSettingsManager)
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

        private static SecureSettingSource GetSecureSettingsManager()
        {
            return new SecureSettingSource(GetSettingSource());
        }

        private static ISettingSource GetSettingSource()
        {
           return SettingSourceFactory.GetSettingSource();
        }

        #endregion

        public string ReadSecureSetting(string name)
        {
            return _secure.ReadSecureSetting(name);
        }

        public void WriteSecureSetting(string key, string value)
        {
            _secure.WriteSecureSetting(key, value);
        }

        public ConnectionStringSettings GetConnectionString(string name)
        {
            return new ConnectionStringSettings
                       {
                           ConnectionString = _secure.GetConnectionString(name),
                           Name = name
                       };
        }

        public void AddConnectionString(string name, string connectionString)
        {
            _secure.SetConnectionString(name, connectionString);
        }

        public void RemoveSecureSetting(string appSettingName)
        {
            _secure.RemoveSecureSetting(appSettingName);
        }

        public void RemoveConnectionString(string name)
        {
            _secure.RemoveConnectionString(name);
        }

        public IEnumerable<ConnectionStringSettings> GetConnectionStrings()
        {
            return (from setting in _secure where setting.Type == "conn" 
                    select new ConnectionStringSettings(setting.Name, setting.Value))
                    .ToList();
        }
    }
}
