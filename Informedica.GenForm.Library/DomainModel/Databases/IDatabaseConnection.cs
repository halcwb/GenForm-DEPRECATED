using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Databases
{
    public interface IDatabaseConnection
    {
        Boolean TestConnection(String connectionString);
        void RegisterSetting(IEnvironment environment);
        String GetConnectionString(String name);
        void SetSettingsPath(string path);
        IEnumerable<String> GetDatabases();
    }
}