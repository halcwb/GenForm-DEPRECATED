namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentFactory
    {
        private const string Filesystem = "FileSystem";

        public static GenFormEnvironment CreateGenFormEnvironment(string machineName, string environmentName, string provider, string connectionString)
        {
            return CreateGenFormEnvironment(machineName, environmentName, provider, connectionString, string.Empty,
                                         string.Empty);
        }

        public static GenFormEnvironment CreateGenFormEnvironment(string machineName, string environmentName, string provider, string connectionString, string logpath, string exportpath)
        {
            var env = Environment.Create(machineName, environmentName);
            env.AddSetting(GenFormEnvironment.Settings.Database.ToString(), provider, connectionString);
            env.AddSetting(GenFormEnvironment.Settings.LogPath.ToString(), Filesystem, logpath);
            env.AddSetting(GenFormEnvironment.Settings.ExportPath.ToString(), Filesystem, exportpath);

            return new GenFormEnvironment(env);
        }
    }
}