using Informedica.GenForm.Acceptance.FitNesse;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Acceptance.Tests
{
    [TestClass]
    public class EnvironmentScenariosTests
    {
        [TestMethod]
        public void ThatAnEnvironmentCanBeAdded()
        {
            var scen = new EnvironmentScenarios();

            Assert.IsTrue(scen.RegisterEnvironmentWithNameAndProviderWithConnectionString("Testing", "SqLite",
                                                                                          "This is a test connection string"));
        }
    }
}
