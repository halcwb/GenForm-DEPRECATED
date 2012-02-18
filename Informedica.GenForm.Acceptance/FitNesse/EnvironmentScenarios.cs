using System;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Settings;

namespace Informedica.GenForm.Acceptance.FitNesse
{
    public class EnvironmentScenarios
    {

        public string GetMachineName()
        {
            return Environment.MachineName;
        }

        public int TheNumberOfEnvironments()
        {
            return new EnvironmentServices().GetEnvironments().Count();
        }

        public string GetListOfEnvironments(int skip)
        {
            return string.Join(", ", GenFormApplication.Environments.Skip(skip).Select(e => e.Name).ToArray());
        }

        public bool SettingNameShouldBe(string name)
        {
            name = name.Replace("MyMachine", Environment.MachineName); 
            var env = GenFormApplication.Environments.Single(e => e.SettingName == name);

            GenFormApplication.Environments.RemoveEnvironment(env);
            return env != null;
        }

        public bool EnvironmentNameShouldBe(string name)
        {
            var envs = new EnvironmentServices().GetEnvironments();
            return envs.Any(e => e.Name == name);
        }

        public bool EnvironmentConnectionStringCanBeSet(string connString)
        {
            return false;
        }

        public bool AddEnvironmentToEnvironmentManager()
        {
            return false;
        }

        public bool CurrentMachineHasProvider(string provider)
        {
            var providers = GenFormApplication.GetRegisterdProviders();
            return providers.Any(p => p.ProviderName == provider);
        }

        public string RegisterEnvironmentWithNameAndProviderWithConnectionString(string name, string provider, string connectionString)
        {
            if (GenFormApplication.GetRegisterdProviders().All(p => p.ProviderName != provider)) return string.Empty;

            var env = new EnvironmentSetting(Environment.MachineName, name, provider, connectionString);
            GenFormApplication.Environments.AddEnvironment(env);

            return env.SettingName;
        }

        public string ProviderForShouldBe(string setname)
        {
            var env = GetEnvironment(setname);

            return env == null ? string.Empty : env.Provider;
        }

        private static EnvironmentSetting GetEnvironment(string settingName)
        {
            var serv = new EnvironmentServices();
            var env = serv.GetEnvironments().Single(e => e.SettingName == settingName);
            return env;
        }

        public string MachineForShouldBe(string settingName)
        {
            var env = GetEnvironment(settingName);

            return env == null ? string.Empty : env.MachineName.Replace(Environment.MachineName, "MyMachine");
        }

        public string EnvironmentSettingForShouldBe(string settingName)
        {
            var env = GetEnvironment(settingName);

            return env == null ? string.Empty : env.SettingName.Replace(env.MachineName, "MyMachine");
        }

        public string CreateEnvironmentSettingWithConnectionString(string settingName, string connectionString)
        {
            var a = settingName.Split('.');
            var setting = new EnvironmentSetting(a[0], a[1], a[2], connectionString);

            return setting.SettingName;
        }

        public string CanCreateEnvironment()
        {
            try
            {
                GenFormApplication.TestSessionFactory.OpenSession();
                return string.Empty;

            }
            catch ( Exception e)
            {
                return e.ToString();
            } 
        }
    }

    public class EnvironmentServices
    {
        public EnvironmentSettings GetEnvironments()
        {
            return GenFormApplication.Environments;

        }
    }
}
