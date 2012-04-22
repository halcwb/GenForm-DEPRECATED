namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentFactory
    {
        public static GenFormEnvironment GetGenFormEnvironment(string name, string machine, string provider, string connectionString)
        {
            var env = Environment.Create(name, machine);
            env.Settings.AddSetting(GenFormEnvironment.Settings.Database.ToString(), provider, connectionString);
            env.Settings.AddSetting(GenFormEnvironment.Settings.LogPath.ToString(), provider);
            env.Settings.AddSetting(GenFormEnvironment.Settings.ExportPath.ToString(), provider);

            return new GenFormEnvironment(env);
        }
    }
}