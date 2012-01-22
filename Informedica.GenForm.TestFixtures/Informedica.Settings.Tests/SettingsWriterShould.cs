using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.Settings.Tests
{
    [TestClass]
    public class SettingsWriterShould
    {
        [TestMethod]
        public void WriteASettingWithAKeyValueUsingConfigurationManager()
        {
            ObjectFactory.Inject(typeof(SettingReader), new ConfigurationManagerSettingReader());
            ObjectFactory.Inject(typeof(SettingWriter), new ConfigurationManagerSettingWriter());

            string key = "Test";
            string value = "Test";
            var writer = CreateSettingWriter();
            writer.WriteSetting(key, value);
            var reader = CreateSettingReader();

            Assert.AreEqual(value, reader.ReadSetting(key));
        }

        [TestMethod]
        public void GetTheGenFormTestConnectionStringUsingSettingsReader()
        {
            ObjectFactory.Inject(typeof(SettingReader), new SettingsSettingReader());

            var connString = CreateSettingReader().ReadSetting("GenFormTest");
            Assert.IsFalse(String.IsNullOrEmpty(connString));
        }

        private SettingReader CreateSettingReader()
        {
            return ObjectFactory.GetInstance<SettingReader>();
        }

        private SettingWriter CreateSettingWriter()
        {
            return ObjectFactory.GetInstance<SettingWriter>();
        }
    }
}
