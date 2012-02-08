namespace Informedica.GenForm.Acceptance.SecureSettingsManager
{
    [TestClass]
    public class CommandLineToolForSsmShould
    {
        private const string TestSecret = "\"This is a test secret\"";

        [TestMethod]
        public void BeAbleToReadASecureKey()
        {
            var runner = new CommandRunner();

            Assert.IsFalse(string.IsNullOrWhiteSpace(runner.GetCommandResult("get.secret")));
        }

        [TestMethod]
        public void BeAbleToSetASecureKey()
        {
            var runner = new CommandRunner();
            var secret = GetSecret(runner);

            Assert.IsTrue(runner.RunOptionWithArguments("set.secret", TestSecret));

            runner.RunOptionWithArguments("set.secret", secret);
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
            var runner = new CommandRunner();
            var secret = GetSecret(runner);

            Assert.IsTrue(runner.RunOptionWithArguments("has.secret", secret));
        }
    }
}
