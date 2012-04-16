using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Settings.Tests.SettingManager
{
    [TestClass]
    public class SettingSourceShould
    {
        [TestMethod]
        public void GetAllSettingsForGenForm()
        {
            var source = SettingSourceFactory.GetSettingSource();

            Assert.IsTrue(source.Any());
        }

    }
}
