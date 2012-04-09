using System.Linq;

namespace Informedica.GenForm.Settings
{
    public class GenFormEnvironment
    {
        private const int DatabaseConnectionStringIndex = 0;
        private const int LogPathIndex = 1;
        private const int ExporthPathIndex = 2;

        private readonly Environment _environment;

        public GenFormEnvironment(Environment environment)
        {
            _environment = environment;
        }

        public string Name { get { return _environment.Name; } }

        public string GenFormDatabaseConnectionString
        {
            get { return _environment.Settings.ElementAt(DatabaseConnectionStringIndex).ConnectionString; }
            set { _environment.Settings.ElementAt(DatabaseConnectionStringIndex).ConnectionString = value; }
        }

        public string LogPath
        {
            get { return _environment.Settings.ElementAt(LogPathIndex).ConnectionString; }
            set { _environment.Settings.ElementAt(LogPathIndex).ConnectionString = value; }
        }

        public string ExportPath
        {
            get { return _environment.Settings.ElementAt(ExporthPathIndex).ConnectionString; }
            set { _environment.Settings.ElementAt(ExporthPathIndex).ConnectionString = value; }
        }
    }
}