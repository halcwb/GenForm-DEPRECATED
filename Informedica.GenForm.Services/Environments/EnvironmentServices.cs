using System.Collections.Generic;
using Informedica.GenForm.Settings;
using Informedica.GenForm.Settings.Environments;

namespace Informedica.GenForm.Services.Environments
{
    public class EnvironmentServices
    {
        public static IEnumerable<Environment> GetEnvironments(string machine)
        {
            return new List<Environment>();
        }

        public static Environment AddNewEnvironment(string name)
        {
            return Environment.Create(System.Environment.MachineName, name);
        }

        public static Environment AddNewEnvironment(string machine, string name)
        {
            return Environment.Create(machine, name);
        }

        public static IEnumerable<Environment> GetEmptyListOfEnvironments()
        {
            return new List<Environment>();
        }
    }
}