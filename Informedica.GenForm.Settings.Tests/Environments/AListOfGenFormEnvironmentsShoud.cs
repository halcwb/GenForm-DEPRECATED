using System.Collections.Generic;
using System.Linq;
using Informedica.SecureSettings;
using Informedica.SecureSettings.Testing;
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
                var genv = TestGenFormEnvironment.CreateTestGenFormEnvironment();
                genv.Database = "Test";
                _environments.AddEnvironment(genv);

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
                var genv = TestGenFormEnvironment.CreateTestGenFormEnvironment();
                _environments.AddEnvironment(genv);
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
            var genv = TestGenFormEnvironment.CreateTestGenFormEnvironment();
            genv.Database = "Test";
            _environments.AddEnvironment(genv);

            Assert.AreEqual((count + 1), _environments.Count());
        }
    }

}