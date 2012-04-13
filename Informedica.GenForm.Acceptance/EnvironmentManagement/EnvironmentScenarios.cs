using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Services.Environments;
using Informedica.GenForm.Settings;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class EnvironmentScenarios
    {
        private Environment _environment;
        private IEnumerable<Environment> _environments;

        public EnvironmentScenarios()
        {
            Init();
        }

        private void Init()
        {
            _environments = new List<Environment>();
        }

        public string GetTheEnvironmentMachineName()
        {
            return _environment.MachineName;
        }

        public bool CanAddNewEnvironmentForMachine(string envName, string machine)
        {
            if (machine == "local machine name") return CanAddNewEnvironment(envName);

            _environment = EnvironmentServices.AddNewEnvironment(envName, machine);
            _environments = EnvironmentServices.GetEnvironments(machine);
            return _environment != null;
        }

        public bool CanAddNewEnvironment(string name)
        {
            _environment = EnvironmentServices.AddNewEnvironment(name);
            _environments = EnvironmentServices.GetEnvironments(System.Environment.MachineName);
            return _environment != null;
        }

        public bool EnvironmentNameIsLocalMachineName()
        {
            return _environment.MachineName == System.Environment.MachineName;
        }

        public bool CreateEmptyListOfEnvironments()
        {
            _environments = EnvironmentServices.GetEmptyListOfEnvironments();
            return _environments != null;
        }

        public bool CreateListOfEnvironmentsWithEnvironments(int numberOfEnvironments)
        {
            var envs = new List<Environment>();
            for (var i = 0; i < numberOfEnvironments; i++)
            {
                envs.Add(Environment.Create("TestMachine", i.ToString(CultureInfo.InvariantCulture)));
            }

            _environments = envs;
            return _environments != null;
        }

        public bool GetListOfEnvironments()
        {
            return _environments != null;
        }

        public int GetNumberOfEnvironments()
        {
            return _environments.Count();
        }

        public bool SetEnvironmentsForMachine(string name)
        {
            _environments = EnvironmentServices.GetEnvironments(name);
            return _environments != null;
        }

        public bool EnvironmentNameIs(string name)
        {
            return _environment.Name == name;
        }

        public string GetMachineName()
        {
            return System.Environment.MachineName;
        }

        public int TheNumberOfEnvironments()
        {
            return EnvironmentServices.GetEnvironments(System.Environment.MachineName).Count();
        }

        public string GetListOfEnvironments(int skip)
        {
            return string.Join(", ", GenFormApplication.Environments.Skip(skip).Select(e => e.Name).ToArray());
        }

        public bool SettingNameShouldBe(string name)
        {
            name = name.Replace("MyMachine", System.Environment.MachineName);
            var env = GenFormApplication.Environments.Single(e => e.Settings.Any(s => s.SettingName == name));

            GenFormApplication.Environments.RemoveEnvironment(env);
            return env != null;
        }

        public bool EnvironmentNameShouldBe(string name)
        {
            var envs = EnvironmentServices.GetEnvironments(System.Environment.MachineName);
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

        public string RegisterEnvironmentWithNameAndProviderWithConnectionString(string machine, string name, string machinename, string environment)
        {
            if (GenFormApplication.GetRegisterdProviders().All(p => p.ProviderName != machinename)) return string.Empty;

            var setting = GetEnvironmentSetting(name, machinename, environment);

            GenFormApplication.Environments.AddEnvironment(Environment.Create(name, machine));
            GenFormApplication.Environments.Single(e => e.Name == name).Settings.AddSetting(setting);

            return setting.SettingName;
        }

        private static EnvironmentSetting GetEnvironmentSetting(string name, string machinename, string environment)
        {
            return EnvironmentSetting.CreateEnvironment(name, machinename, environment);
        }

        public string ProviderForShouldBe(string setname)
        {
            var env = GetEnvironmentSetting(setname);

            return env == null ? string.Empty : env.Provider;
        }

        private static EnvironmentSetting GetEnvironmentSetting(string settingName)
        {
            var machine = "test";
            var envName = "test";
            return EnvironmentServices
                .GetEnvironments(machine)
                .Single(e => e.Name == envName)
                .Settings
                .Single(s => s.SettingName == settingName);
        }

        public string MachineForShouldBe(string settingName)
        {
            var env = GetEnvironmentSetting(settingName);

            return env == null ? string.Empty : env.MachineName.Replace(System.Environment.MachineName, "MyMachine");
        }

        public string EnvironmentSettingForShouldBe(string settingName)
        {
            var env = GetEnvironmentSetting(settingName);

            return env == null ? string.Empty : env.SettingName.Replace(env.MachineName, "MyMachine");
        }

        public string CreateEnvironmentSettingWithConnectionString(string settingName, string connectionString)
        {
            var a = settingName.Split('.');
            var setting = CreateEnvironmentSetting(a[0], a[1], a[2], settingName, connectionString);

            return setting.SettingName;
        }

        private EnvironmentSetting CreateEnvironmentSetting(string name, string machinename, string environment, string provider, string connectionString)
        {
            var envset = EnvironmentSetting.CreateEnvironment(name, machinename, environment);
            envset.Provider = provider;
            envset.ConnectionString = connectionString;

            return envset;
        }
    }
}