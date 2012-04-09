using TypeMock.ArrangeActAssert;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Settings.Tests
{
    [TestClass]
    public class AListOfGenFormEnvironmentsShoud
    {
        private GenFormEnvironments _environments;

        [TestInitialize]
        public void SetUpGenFormEnvironments()
        {
            _environments = new GenFormEnvironments();
        }

        [TestMethod]
        public void OnlyAddANewGenFormEnvironmentWithAName()
        {
            AddNewGenFormEnvironment();

            AssertIsNotNullOrWhiteSpace(_environments.ElementAt(0).Name);
        }

        [TestMethod]
        public void OnlyAddNewGenFormEnvironmentWithDatabaseConnectionString()
        {
            AddNewGenFormEnvironment();

            AssertIsNotNullOrWhiteSpace(_environments.ElementAt(0).GenFormDatabaseConnectionString);
        }

        private static void AssertIsNotNullOrWhiteSpace(string value)
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(value));
        }

        [TestMethod]
        public void HaveACountIncreasedWithOneWhenANewGenFormEnvironmetIsAdded()
        {
            var count = _environments.Count();
            AddNewGenFormEnvironment();

            Assert.AreEqual((count + 1), _environments.Count());
        }

        private void AddNewGenFormEnvironment()
        {
            var genv = _environments.AddNewEnvironment("Test");
        }
    }

}
