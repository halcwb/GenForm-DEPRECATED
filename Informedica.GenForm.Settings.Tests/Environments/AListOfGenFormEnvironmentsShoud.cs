using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Informedica.GenForm.Settings.Environments;
using Informedica.GenForm.Settings.Tests.SettingsManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class AListOfGenFormEnvironmentsShoud : SecureSettingSourceTestFixture
    {
        private ICollection<GenFormEnvironment> _environments;

        [TestInitialize]
        public void SetUpGenFormEnvironments()
        {
            _environments = new GenFormEnvironmentCollection();
        }

        [TestMethod]
        public void OnlyAddANewGenFormEnvironmentWithAName()
        {
            try
            {
                var genv = TestGenFormEnvironmentFactory.CreateTestGenFormEnvironment();
                genv.Database = "Test";
                _environments.Add(genv);

            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        [Isolated]
        public void UseAEnvironmentsCollectionToGetTheGenFormEnvironments()
        {
            
            var envCol = Isolate.Fake.Instance<EnvironmentCollection>();
            var genformenvs = new List<Environment>();
            var fakeEnv = Isolate.Fake.Instance<Environment>();
            genformenvs.Add(fakeEnv);
            Isolate.WhenCalled(() => fakeEnv.Name).WillReturn("TestEnvironment");
            Isolate.WhenCalled(() => fakeEnv.MachineName).WillReturn("MyMachine");
            
            Isolate.WhenCalled(() => envCol.GetEnvironmentsForMachine("MyMachine")).CallOriginal();

            var col = new GenFormEnvironmentCollection(envCol);
        }
    }

}
