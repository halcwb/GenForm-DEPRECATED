using System;
using Informedica.GenForm.Acceptance.FitNesse;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Acceptance.Tests
{
    [TestClass]
    public class EnvironmentScenariosTests
    {
        private const string Testing = "Testing";

        [TestMethod]
        public void ThatAnEnvironmentCanBeAdded()
        {
            var scen = new EnvironmentScenarios();

            Assert.IsTrue(RunRegisterEnvironmentScenario(scen));
        }

        private static bool RunRegisterEnvironmentScenario(EnvironmentScenarios scen)
        {
            return scen.RegisterEnvironmentWithNameAndProviderWithConnectionString(Testing, "SqLite",
                                                                                   "This is a test connection string");
        }

        [TestMethod]
        public void WhenEnvironmentNameIsAddedNameShouldBe()
        {
            var scen = new EnvironmentScenarios();
            RunRegisterEnvironmentScenario(scen);

            var expected = "MyMachine." + Testing + ".SqLite";
            Assert.IsTrue(scen.SettingNameShouldBe(expected));
        }
    }
}
