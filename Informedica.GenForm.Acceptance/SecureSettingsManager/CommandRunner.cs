namespace Informedica.GenForm.Acceptance.SecureSettingsManager
{
    public class CommandRunner
    {
        private static SecureSettings.CommandRunner runner = new SecureSettings.CommandRunner();

        public bool RunCommandWithArgument(string option, string arg)
        {
            return runner.RunCommandWithArgument(option, arg);
        }

        public string GetCommandResult(string arg)
        {
            return runner.GetCommandResult(arg);
        }

        public bool CheckCommandLine()
        {
            return RunCommandWithArgument(string.Empty, string.Empty);
        }
    }

}
