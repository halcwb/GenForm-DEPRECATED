using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests
{
    [TestClass]
    public class AGenFormEnvironmentShould
    {
        [TestMethod]
        public void HaveANameThatCanBeSetToTest()
        {
            var name = "Test";
            var env = EnvironmentManager.CreateNewEnvironment(name);

            Assert.AreEqual(name, env.Name);
        }

        [TestMethod]
        public void GenFormEnvironmentShouldUseAnEnvironmentToGetTheName()
        {
            var name = "Test";
            var env = Isolate.Fake.Instance<Environment>();
            var genv = new GenFormEnvironment(env);

            Isolate.WhenCalled(() => genv.Name).WillReturn(name);
            Isolate.Verify.WasCalledWithAnyArguments(() => env.Name);
            
            Assert.AreEqual(name, genv.Name);
        }
    }

    public static class EnvironmentManager
    {
        public static GenFormEnvironment CreateNewEnvironment(string name)
        {
            return new GenFormEnvironment(new Environment(name));
        }
    }

    public class GenFormEnvironment
    {
        private Environment _environment;

        public GenFormEnvironment(Environment environment)
        {
            _environment = environment;
        }

        public string Name { get { return _environment.Name; } }
    }
}
