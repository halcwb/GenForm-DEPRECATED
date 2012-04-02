using System.Collections.Generic;
using Informedica.GenForm.Settings;

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
            return Environment.Create(name);
        }

        public static Environment AddNewEnvironment(string name, string machine)
        {
            return Environment.Create(name, machine);
        }

        public static IEnumerable<Environment> GetEmptyListOfEnvironments()
        {
            return new List<Environment>();
        }
    }
}