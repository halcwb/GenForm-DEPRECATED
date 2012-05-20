namespace Informedica.GenForm.Services.Environments
{
    public class EnvironmentDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Database { get; set; }

        public string LogPath { get; set; }
            
        public string ExportPath { get; set; }

        public string provider { get; set; }
    }
}