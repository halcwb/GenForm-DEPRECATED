using System;

namespace Informedica.GenForm.Library.DomainModel.Databases
{
    public interface IDatabaseConnection
    {
        Boolean TestConnection(String connectionString);
        void RegisterSetting(IDatabaseSetting databaseSetting);
    }
}