namespace Informedica.GenForm.Settings.Environments
{
    public class EnvironmentFactory
    {
        public static GenFormEnvironment GetGenFormEnvironment(string name, string machine, string connectionString)
        {
            var env = Environment.Create(name, machine);
            env.Settings.AddSetting(GenFormEnvironment.Settings.Database.ToString(), connectionString);
            env.Settings.AddSetting(GenFormEnvironment.Settings.LogPath.ToString());
            env.Settings.AddSetting(GenFormEnvironment.Settings.ExportPath.ToString());
            return new GenFormEnvironment(env);
        }
    }
}