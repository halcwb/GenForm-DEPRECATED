using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Databases;

namespace Informedica.GenForm.Library.Services.Databases
{
    public interface IDatabaseServices

    {
        Boolean TestDatabaseConnection(IDatabaseSetting databaseSetting);
        Boolean RegisterDatabaseSetting(IDatabaseSetting databaseSetting);
        void MapSettingsPath(String path);
        IEnumerable<String> GetDatabases();
    }
}