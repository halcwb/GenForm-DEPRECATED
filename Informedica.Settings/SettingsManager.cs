using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using StructureMap;

namespace Informedica.Settings
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

        private const string Environments = "environments";
        private const string ConnectionString = "connectionString";
        private const string EnvironmentElement = "environment";
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
                        var instance = new SettingsManager();
                        Thread.MemoryBarrier();
                        _instance = instance;
                        //_instance.Initialize();
                    }
                    return _instance;
                }
            }
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

        private string EncryptSetting(string value)
        {
            _crypt.Key = _key;
            return _crypt.Encrypt(value);

        }

        public string ReadSecureSetting(string name)
        {
            return GetSettingReader().ReadSetting(name);
        }

        private SettingReader GetSettingReader()
        {
            return ObjectFactory.GetInstance<SettingReader>();
        }

        private string GetEnvirionment(XElement xElement)
        {
            if (xElement == null) return String.Empty;

            var xAttribute = xElement.Attribute(ConnectionString);
            if (xAttribute != null) return xAttribute.Value;
            return String.Empty;
        }

        public void WriteSecureSetting(string key, string value)
        {
            GetSettingWriter().WriteSetting(key, value);
        }

        private ConfigurationManagerSettingWriter GetSettingWriter()
        {
            return new ConfigurationManagerSettingWriter();
        }

        private static void ChangeEnvironment(XElement environment, string connectionString)
        {
            var xAttribute = environment.Attribute(ConnectionString);
            if (xAttribute != null)
                xAttribute.Value = connectionString;
        }

        private void AddEnvironment(string key, string value)
        {
            EnvironmentsRoot.Add((new XElement(EnvironmentElement, 
                                      new XAttribute(EnvironmentName, key), 
                                      new XAttribute(ConnectionString, value))));
        }

        private XElement FindEnvironmentElement(string environmentName)
        {
            return EnvironmentsRoot.Elements().Count() == 0 ? null : EnvironmentsRoot.Elements().SingleOrDefault(
                x =>
                {
                    var xAttribute = x.Attribute(EnvironmentName);
                    return xAttribute != null && xAttribute.Value == environmentName;
                });
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
    }

    public class SettingsManagerException : Exception
    {
        public SettingsManagerException(string file, Exception exception) : base("Could not create: " + file + " throws error:" + exception)
        {
            
        }
    }
}
