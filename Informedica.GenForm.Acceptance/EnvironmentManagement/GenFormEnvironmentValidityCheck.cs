using Informedica.GenForm.Settings;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class GenFormEnvironmentValidityCheck
    {
        private GenFormEnvironment _environment;

        public GenFormEnvironmentValidityCheck()
        {
            SetEnvironment(string.Empty);
        }

        private void SetEnvironment(string name)
        {
            _environment = new GenFormEnvironment(new Environment(name));
        }

        public string Name { get; set; }
        public string DatabaseConnection { get; set; }
        public string DatabaseProvider { get; set; }
        public string LogPath { get; set; }
        public string ExportPath { get; set; }

        public string IsValid()
        {
            SetEnvironment(Name);
            _environment.GenFormDatabaseConnectionString = DatabaseConnection;

            return _environment.GenFormDatabaseConnectionString; //_environment.IsValid() ? "Yes" : "No";
        }
    }
}
