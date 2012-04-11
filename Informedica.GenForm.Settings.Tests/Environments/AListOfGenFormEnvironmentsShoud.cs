using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Settings.Tests.Environments
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
            try
            {
                AddNewGenFormEnvironment();
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void OnlyAddNewGenFormEnvironmentWithDatabaseConnectionString()
        {
            try
            {
                AddNewGenFormEnvironment();
                Assert.Fail("Should throw an error");
            }
            catch (System.Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }

        [TestMethod]
        public void HaveACountIncreasedWithOneWhenANewGenFormEnvironmentIsAdded()
        {
            var count = _environments.Count();
            AddNewGenFormEnvironment();

            Assert.AreEqual((count + 1), _environments.Count());
        }

        private void AddNewGenFormEnvironment()
        {
            var genv = _environments.CreateNewEnvironment("Test", "Test");
            genv.GenFormDatabaseConnectionString = "Some connection string";
            _environments.AddEnvironment(genv);
        }
    }

}
