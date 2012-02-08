namespace Informedica.GenForm.Acceptance.SecureSettingsManager
{
    public class CommandRunner
    {
        private static SecureSettings.CommandRunner runner = new SecureSettings.CommandRunner();

        public bool RunOptionWithArguments(string option, string arguments)
        {
            return runner.RunOptionWithArguments(option, arguments);
        }

        public string GetCommandResult(string optsargs)
        {
            return runner.GetCommandResult(optsargs);
        }

        public bool CheckCommandLine()
        {
            return GetCommandResult(string.Empty).Contains("Options are");
        }

        public bool SetSecret(string secret)
        {
            var original = GetCommandResult("get.secret");
            var result = RunOptionWithArguments("set.secret", secret);

            RunOptionWithArguments("set.secret", original);

            return result;
        }
    }

}
