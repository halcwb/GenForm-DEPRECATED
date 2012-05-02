namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentFactory
    {
        public static GenFormEnvironment GetGenFormEnvironment(string machineName, string environmentName, string provider, string connectionString)
        {
            var env = Environment.Create(machineName, environmentName);
            env.AddSetting(GenFormEnvironment.Settings.Database.ToString(), provider, connectionString);
            env.AddSetting(GenFormEnvironment.Settings.LogPath.ToString(), provider);
            env.AddSetting(GenFormEnvironment.Settings.ExportPath.ToString(), provider);

            return new GenFormEnvironment(env);
        }
    }
}