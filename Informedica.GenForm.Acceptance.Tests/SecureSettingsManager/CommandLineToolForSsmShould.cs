using Informedica.GenForm.Acceptance.SecureSettingsManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Acceptance.Tests.SecureSettingsManager
{
    [TestClass]
    public class CommandLineToolForSsmShould
    {
        private const string Secret = "secret";

        [TestMethod]
        public void BeAbleToSetASecureKey()
        {
            var runner = new CommandRunner();
            
            Assert.IsTrue(runner.RunCommandWithArgument("set.secret", Secret));
        }

        [TestMethod]
        public void ReturnTrueWhenSecretIsSecret()
        {
            var runner = new CommandRunner();
            runner.RunCommandWithArgument("set.secret", Secret);

            Assert.IsTrue(runner.RunCommandWithArgument("has.secret",Secret));
        }
    }
}
