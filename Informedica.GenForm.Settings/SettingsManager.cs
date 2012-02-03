using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web.Configuration;
using System.Xml.Linq;
using Informedica.SecureSettings;
using StructureMap;

namespace Informedica.GenForm.Settings
{
    public class SettingsManager
    {
        public const string GenformsettingsXml = "GenFormSettings.xml";
        static SettingsManager _instance;
        static readonly object LockThis = new object();

        private XDocument _settingsDoc = new XDocument();
        private string _path = @"C:\Users\halcwb\Documents\Visual Studio 2010\Projects\GenForm\Informedica.GenForm.Mvc3\";

        private readonly string _key = SecurityKey.Key;
        private readonly SymCryptography _crypt = new SymCryptography(SymCryptography.ServiceProviderEnum.Rijndael);
        private SecureSettingsManager _secure;

        public SettingsManager(SecureSettingsManager secureSettingsManager)
        {
            _secure = secureSettingsManager;
        }

        private const string Environments = "environments";
        private const string EnvironmentName = "name";

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
                        //_instance.Initialize();
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

        public void Initialize(string path)
        {
            _path = path;
            var file = GetFile();
            if (!System.IO.File.Exists(file))
            {
                file = CreateFile();
                Initialize();
            }
            _settingsDoc = XDocument.Load(file);
        }

        public void Initialize()
        {
            var file = GetFile();
            if (!System.IO.File.Exists(file))
            {
                file = CreateFile();
                Initialize();
                throw new Exception("Could not find settings file in path: " + file);
            }
            TryLoadFile(file);
        }

        private void TryLoadFile(string file)
        {
            try
            {
                _settingsDoc = XDocument.Load(file);

            }
            catch (Exception)
            {
                CreateFile();
                _settingsDoc = XDocument.Load(GetFile());
            }
        }

        private string CreateFile()
        {
            _settingsDoc = new XDocument();
            var file = _path + GenformsettingsXml;

            try
            {
                _settingsDoc.AddFirst(new XElement(Environments));
                _settingsDoc.Save(file);

            }
            catch (Exception e)
            {
                throw new SettingsManagerException(file, e);
            }            
            return file;
        }

        private string GetFile()
        {
            FileFinder.Filter = new List<string> {_path};
            var file = FileFinder.FindPath(GenformsettingsXml).FirstOrDefault();
            return file;
        }

        public string GetSettingsPath()
        {
            return _path;
        }

        public string EncryptMachineName(string computerName)
        {
            _crypt.Key = _key;
            if (computerName == "")
            {
                computerName = Environment.MachineName;
            }
            return computerName; //_crypt.Encrypt(computerName.ToLower());
        }

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

        public IEnumerable<String> GetNames()
        {
            return (from element in EnvironmentElements 
                    select element.Attribute(EnvironmentName) into attr 
                    where attr != null 
                    select _crypt.Decrypt(attr.Value)).ToList();
        }

        private XElement EnvironmentsRoot
        {
            get
            {
                var xElement = _settingsDoc.Element(Environments);
                if (xElement == null)
                {
                    xElement = new XElement(Environments);
                    _settingsDoc.Add(xElement);
                }

                return xElement;
            }
        }

        private IEnumerable<XElement> EnvironmentElements
        {
            get { return EnvironmentsRoot.Elements(); }
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

    public class SettingSource: ISettingSource
    {
        private static IList<Setting> _settings = new List<Setting>();
        private static Configuration _configuration = WebConfigurationManager.OpenWebConfiguration("/GenForm");

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<Setting> GetEnumerator()
        {
            _settings.Clear();
            foreach (ConnectionStringSettings connstr in _configuration.ConnectionStrings.ConnectionStrings)
            {
                _settings.Add(new Setting(connstr.Name, connstr.ConnectionString));
            }
            return _settings.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ISettingSource

        public void WriteConnectionString(string name, string connectionString)
        {
            var conset = _configuration.ConnectionStrings.ConnectionStrings[name];
            if (conset == null)
            {
                conset = new ConnectionStringSettings(name, connectionString);
                _configuration.ConnectionStrings.ConnectionStrings.Add(conset);
            }
            else
            {
                _configuration.ConnectionStrings.ConnectionStrings[name].ConnectionString = connectionString;
            }
            _configuration.Save();
        }

        public string ReadConnectionString(string name)
        {
            return _configuration.ConnectionStrings.ConnectionStrings[name].ConnectionString;
        }

        public void WriteAppSetting(string name, string setting)
        {
            throw new NotImplementedException();
        }

        public string ReadAppSetting(string name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class SettingsManagerException : Exception
    {
        public SettingsManagerException(string file, Exception exception) : base("Could not create: " + file + " throws error:" + exception)
        {
            
        }
    }
}
