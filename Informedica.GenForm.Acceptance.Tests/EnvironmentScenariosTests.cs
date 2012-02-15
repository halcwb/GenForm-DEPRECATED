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
            var setName = scen.RegisterEnvironmentWithNameAndProviderWithConnectionString(Testing, "SqLite",
                                                                                   "This is a test connection string");
            return !string.IsNullOrWhiteSpace(setName);
        }

        [TestMethod]
        public void WhenEnvironmentNameIsAddedNameShouldBe()
        {
            var scen = new EnvironmentScenarios();
            RunRegisterEnvironmentScenario(scen);

            var expected = "MyMachine." + Testing + ".SqLite";
            Assert.IsTrue(scen.SettingNameShouldBe(expected));
        }

        [TestMethod]
        public void WhenEnvironmentWithNameTestingIsAddedNameShouldBeTesting()
        {
            var scen = new EnvironmentScenarios();
            RunRegisterEnvironmentScenario(scen);

            Assert.IsTrue(scen.EnvironmentNameShouldBe(Testing));
        }
    }
}
