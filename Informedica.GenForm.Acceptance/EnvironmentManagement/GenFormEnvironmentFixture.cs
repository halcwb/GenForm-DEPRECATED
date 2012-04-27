using System;
using Informedica.GenForm.Settings.Environments;

namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class GenFormEnvironmentFixture
    {
        private GenFormEnvironment _environment;
        protected string MachineName;
        protected string EnvironmentName;
        protected string ConnectionString;
        protected string Provider;

        public string Machine
        {
            get { return Environment == null ? string.Empty : Environment.MachineName; }
            set 
            { 
                MachineName = value;
                _environment = null;
            }
        }

        public string Name
        {
            get { return Environment ==  null ? string.Empty : Environment.Name; }
            set
            {
                EnvironmentName = value;
                _environment = null;
            }
        }

        public string DatabaseConnection
        {
            get { return Environment == null ? string.Empty : Environment.Database; }
            set
            {
                ConnectionString = value;
                _environment = null;
            }
        }

        public string DatabaseProvider
        {
            get { return Environment == null ? string.Empty : Environment.Provider; }
            set
            {
                _environment = null;
                Provider = value;
            }
        }

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

        protected GenFormEnvironment Environment
        {
            get { return _environment ?? (_environment = TryCreateEnvironment()); }
        }

        private GenFormEnvironment TryCreateEnvironment()
        {
            try
            {
                return EnvironmentFactory.GetGenFormEnvironment(EnvironmentName, MachineName, Provider, ConnectionString);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}