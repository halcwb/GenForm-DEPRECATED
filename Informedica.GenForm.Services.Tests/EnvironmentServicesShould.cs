using System.Linq;
using Informedica.GenForm.Services.Environments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Services.Tests
{
    [TestClass]
    public class EnvironmentServicesShould
    {
        [TestMethod]
        public void ReturnAListOfGenFormEnvironmentsWithAtLeastATestGenForm()
        {
            var list = EnvironmentServices.GetEnvironments();
            Assert.IsTrue(list.Any(e => e.Name == "TestGenForm"));
        }
    }
}
