using System;
using Informedica.SecureSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Acceptance.SecureSettingsManager
{
    [TestClass]
    public class CommandLineToolForScsmShould
    {
        private const string TestSecret = "\"This is a test secret\"";
        private static CommandRunner _runner = new CommandRunner();

        [TestMethod]
        public void BeAbleToReadASecureKey()
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(_runner.GetCommandResult("get.secret")));
        }

        [TestMethod]
        public void BeAbleToSetASecureKey()
        {
            var secret = GetSecret(_runner);

            Assert.IsTrue(_runner.RunOptionWithArguments("set.secret", TestSecret));

            _runner.RunOptionWithArguments("set.secret", secret);
        }

        private static string GetSecret(CommandRunner runner)
        {
            var secret = "\"" + runner.GetCommandResult("get.secret") + "\"";
            secret = secret.Replace("success: ", "").Replace("\n", "").Replace("\r", "");
            return secret;
        }

        [TestMethod]
        public void ReturnTrueWhenSecretIsSecret()
        {
            var secret = GetSecret(_runner);

            Assert.IsTrue(_runner.RunOptionWithArguments("has.secret", secret));
        }

        [TestMethod]
        public void BeAbleToRun()
        {
            try
            {
                new CommandRunner();
            }
            catch(Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [TestMethod]
        public void ReturnOptionList()
        {
            var result = _runner.GetCommandResult(string.Empty);
            Assert.IsTrue(result.Contains("Options"));
        }
    }
}
