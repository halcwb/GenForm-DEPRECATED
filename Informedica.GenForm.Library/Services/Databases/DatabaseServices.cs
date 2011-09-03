using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Databases;

namespace Informedica.GenForm.Library.Services.Databases
{
    public static class DatabaseServices
    {
        #region Implementation of IDatabaseServices

        public static bool TestDatabaseConnection(IDatabaseSetting databaseSetting)
        {
            return GetDatabaseConnection().TestConnection(databaseSetting.ConnectionString);
        }

        public static bool RegisterDatabaseSetting(IDatabaseSetting databaseSetting)
        {
            if(!TestDatabaseConnection(databaseSetting)) return  false;

            GetDatabaseConnection().RegisterSetting(databaseSetting);
            return true;
        }

        public static void MapSettingsPath(String path)
        {
            GetDatabaseConnection().SetSettingsPath(path);
        }

        public static IEnumerable<string> GetDatabases()
        {
            return GetDatabaseConnection().GetDatabases();
        }

        private static IDatabaseConnection GetDatabaseConnection()
        {
            return Factory.ObjectFactory.Instance.GetInstance<IDatabaseConnection>();
        }

        #endregion
    }
}