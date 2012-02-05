using System;
using System.Configuration;
using System.Web.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    [TestClass]
    public class WebConfigurationManagerShould
    {
        private const string Testsetting = "testSetting";

        [TestMethod]
        public void BeAbleToOpenASpecificWebConfig()
        {
            try
            {
                WebConfigurationManager.OpenWebConfiguration("/GenForm");
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            } 
        }

        [TestMethod]
        public void BeAbleToAddAndRemoveAConnectionString()
        {
            var man = GetWebConfiguration();
            var testSetting = new ConnectionStringSettings(Testsetting, "this is a test connectionstring");

            man.ConnectionStrings.ConnectionStrings.Add(testSetting);
            man.Save();
            Assert.IsNotNull(man.ConnectionStrings.ConnectionStrings[Testsetting]);

            man = GetWebConfiguration();
            man.ConnectionStrings.ConnectionStrings.Remove(Testsetting);
            man.Save();
            Assert.IsNull(man.ConnectionStrings.ConnectionStrings[Testsetting]);
        }

        [TestMethod]
        public void BeAbleToReadTheAppSettings()
        {
            var man = GetWebConfiguration();
            Assert.IsTrue(man.AppSettings.Settings.Count > 0);
        }

        [TestMethod]
        public void BeAbleToGetTheInformedicaSectionGroup()
        {
            var man = GetWebConfiguration();

            try
            {
                Assert.IsFalse(String.IsNullOrWhiteSpace(man.SectionGroups["Informedica"].Sections[0].SectionInformation.Name));
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        private static Configuration GetWebConfiguration()
        {
            var man = WebConfigurationManager.OpenWebConfiguration("/GenForm");
            return man;
        }

    }
}
