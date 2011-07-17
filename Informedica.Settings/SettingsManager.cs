using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml;

namespace Informedica.Settings
{
    public class SettingsManager
    {
        static SettingsManager _instance;
        static readonly object LockThis = new object();

        private string _path = @"C:\Users\halcwb\Documents\Visual Studio 2010\Projects\GenForm\Informedica.GenForm.Mvc3\";
        private readonly XmlDocument _settingsDoc = new XmlDocument();
        
        private readonly string _key = SecurityKey.Key;

        private readonly SymCryptography _crypt = new SymCryptography(SymCryptography.ServiceProviderEnum.Rijndael);

        #region Singleton

        private SettingsManager()
        {}

        public static SettingsManager Instance
        {
            get
            {
                lock (LockThis)
                {
                    if (_instance == null)
                    {
                        _instance = new SettingsManager();
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
            string file = _path + "Settings.xml";
            if (!System.IO.File.Exists(file))
            {
                throw new Exception("Could not find settings file in path: " + file);
            }
            _settingsDoc.Load(file);
        }

        public void Initialize()
        {
            string file = _path + "Settings.xml";
            if (!System.IO.File.Exists(file))
            {
                throw new Exception("Could not find settings file in path: " + file);
            }
            _settingsDoc.Load(file);
        }

        public string GetSettingsPath()
        {
            return _path;
        }
        public string GetSecureMachineName(string computerName)
        {
            _crypt.Key = _key;
            if (computerName == "")
            {
                computerName = Environment.MachineName;
            }
            return computerName; //_crypt.Encrypt(computerName.ToLower());
        }

        private string GetSecureSetting(string value)
        {
            _crypt.Key = _key;
            return _crypt.Encrypt(value);

        }

        public string ReadSecureSetting(string name)
        {
            string machineCrypt = GetSecureMachineName("");
            string machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']/" + name;
            XmlNode machineNode = _settingsDoc.SelectSingleNode(machineStr);
            if (machineNode == null)
            {
                throw new Exception("The setting " + name + " could not be found for this machine: " + machineCrypt);
            }
            _crypt.Key = _key;
            return _crypt.Decrypt(machineNode.InnerText);
        }

        public void CreateSecureSetting(string computerName, string name, string value)
        {
            if (value == "") return;

            string xmlPathServer = "/settings/serversettings[1]";
            XmlNode serverNode = _settingsDoc.SelectSingleNode(xmlPathServer);
            string machineCrypt = GetSecureMachineName(computerName);
            
            string machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']";
            XmlNode machineNode = _settingsDoc.SelectSingleNode(machineStr);
            if (machineNode == null)
            {
                XmlNode newMachineNode = _settingsDoc.CreateElement("machine");
                if (serverNode != null) serverNode.AppendChild(newMachineNode);
                XmlNode keyNode = _settingsDoc.CreateElement("key");
                keyNode.InnerText = machineCrypt;
                newMachineNode.AppendChild(keyNode);
                machineNode = newMachineNode;
            }
            else
            {
                string appstr = "/settings/serversettings/machine[key='" + machineCrypt + "']/" + name;
                XmlNode appnode = _settingsDoc.SelectSingleNode(appstr);
                if (appnode != null)
                {
                    if (appnode.ParentNode != null) appnode.ParentNode.RemoveChild(appnode);
                }
            }
            XmlNode newSettingsNode = _settingsDoc.CreateElement(name);
            newSettingsNode.InnerText = GetSecureSetting(value);
            machineNode.AppendChild(newSettingsNode);
            _settingsDoc.Save(_path + "Settings.xml");
        }

        public IEnumerable<String> GetNames()
        {
            var list = new List<String>();
            foreach (System.Xml.XmlElement node in _settingsDoc.GetElementsByTagName("machine"))
            {
                foreach (System.Xml.XmlElement child in node.ChildNodes)
                {
                    if (child.Name != "key") list.Add(child.Name);
                }
            }
            return list;
        }
    }
}
