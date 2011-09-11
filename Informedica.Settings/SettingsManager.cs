﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace Informedica.Settings
{
    public class SettingsManager
    {
        public const string GenformsettingsXml = "GenFormSettings.xml";
        static SettingsManager _instance;
        static readonly object LockThis = new object();

        private XDocument _settingsDoc = new XDocument();
        private string _path = @"Users\halcwb\Documents\Visual Studio 2010\Projects\GenForm\";

        private readonly string _key = SecurityKey.Key;
        private readonly SymCryptography _crypt = new SymCryptography(SymCryptography.ServiceProviderEnum.Rijndael);

        private const string Drive = @"C:/";
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
                        _instance.Initialize();
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
                throw new Exception("Could not find settings file in path: " + file);
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
            var file = Drive + _path + GenformsettingsXml;
            //_settingsDoc.
            //_settingsDoc.AppendChild(_settingsDoc.CreateElement("environments"));
            _settingsDoc.AddFirst(new XElement(Environments));
            _settingsDoc.Save(file);
            
            return file;
        }

        private string GetFile()
        {
            FileFinder.Filter = new List<string> {_path};
            string file = FileFinder.FindPath(GenformsettingsXml).FirstOrDefault();
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
            _crypt.Key = _key;
            return _crypt.Decrypt("test");
        }

        public void WriteSecureSetting(string key, string value)
        {
            if (String.IsNullOrWhiteSpace(key) || String.IsNullOrWhiteSpace(value)) return;

            key = EncryptSetting(key);
            value = EncryptSetting(value);

            var element = FindEnvironmentElement(key);

            if (element == null) AddEnvironment(key, value);
            else ChangeEnvironment(element, value);

            _settingsDoc.Save(GetFile());
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
}
