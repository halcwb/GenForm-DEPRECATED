using System.Collections.Generic;

namespace Informedica.GenForm.Settings
{
    public class Environment
    {
        private readonly IList<EnvironmentSetting> _settings;

        public Environment(string name)
        {
            Name = name;
            MachineName = System.Environment.MachineName;
        }

        public Environment(string name, IList<EnvironmentSetting> settings)
        {
            Name = name;
            _settings = settings;
            MachineName = System.Environment.MachineName;
        }

        public Environment(string name, string machine)
        {
            Name = name;
            MachineName = machine;
        }

        public static Environment Create(string name)
        {
            return new Environment(name);
        }

        public string Name { get; private set; }

        public IEnumerable<EnvironmentSetting> Settings
        {
            get { return _settings; }
        }

        public string MachineName { get; private set; }

        public static Environment Create(string name, string machine)
        {
            return new Environment(name, machine);
        }
    }
}