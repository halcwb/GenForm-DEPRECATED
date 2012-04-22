using System;
using Informedica.GenForm.Settings.Environments;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class GenFormEnvironmentValidityCheck : fit.ColumnFixture
    {
        private GenFormEnvironment _environment;
        private string _machineName;
        private string _name;
        private string _connectionString;

        public string Machine
        {
            get { return Environment == null ? string.Empty : Environment.MachineName; }
            set 
            { 
                _machineName = value;
                _environment = null;
            }
        }

        public string Name
        {
            get { return Environment ==  null ? string.Empty : Environment.Name; }
            set
            {
                _name = value;
                _environment = null;
            }
        }

        public string DatabaseConnection
        {
            get { return Environment == null ? string.Empty : Environment.Database; }
            set
            {
                _connectionString = value;
                _environment = null;
            }
        }

        public string DatabaseProvider { get; set; }

        public string LogPath
        {
            get { return Environment == null ? string.Empty : Environment.LogPath; } 
            set { if (Environment != null) Environment.LogPath = value; }
        }

        public string ExportPath
        {
            get { return Environment == null ? string.Empty : Environment.ExportPath; } 
            set { if (Environment != null) Environment.ExportPath = value; }
        }

        private GenFormEnvironment Environment
        {
            get { return _environment ?? (_environment = TryCreateEnvironment()); }
        }

        private GenFormEnvironment TryCreateEnvironment()
        {
            try
            {
               return EnvironmentFactory.GetGenFormEnvironment(_name, _machineName, _connectionString);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string IsValid()
        {
            return Environment == null ? "No" : "Yes";
        }
    }
}
