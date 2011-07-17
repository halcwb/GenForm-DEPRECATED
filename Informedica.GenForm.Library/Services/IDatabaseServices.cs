using System;
using Informedica.GenForm.Library.DomainModel.Databases;

namespace Informedica.GenForm.Library.Services
{
    public interface IDatabaseServices

    {
        Boolean TestDatabaseConnection(IDatabaseSetting databaseSetting);
        Boolean RegisterDatabaseSetting(IDatabaseSetting databaseSetting);
    }
}