using Informedica.GenForm.Settings;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class GenFormEnvironmentValidityCheck
    {
        private GenFormEnvironment _environment;

        public GenFormEnvironmentValidityCheck()
        {
            SetEnvironment(string.Empty, string.Empty);
        }

        private void SetEnvironment(string name, string machine)
        {
            _environment = new GenFormEnvironment(new Environment(name, machine));
        }

        public string Machine { get; set; }
        public string Name { get; set; }
        public string DatabaseConnection { get; set; }
        public string DatabaseProvider { get; set; }
        public string LogPath { get; set; }
        public string ExportPath { get; set; }

        public string IsValid()
        {
            SetEnvironment(Name, Machine);
            _environment.GenFormDatabaseConnectionString = DatabaseConnection;

            return _environment.GenFormDatabaseConnectionString; //_environment.IsValid() ? "Yes" : "No";
        }
    }
}
