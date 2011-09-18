using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Databases;

namespace Informedica.GenForm.Library.Services.Databases
{
    public static class EnvironmentServices
    {
        #region Implementation of IDatabaseServices

        public static bool TestDatabaseConnection(IEnvironment environment)
        {
            return GetDatabaseConnection().TestConnection(environment.ConnectionString);
        }

        public static bool RegisterEnvironment(IEnvironment environment)
        {
            if(!TestDatabaseConnection(environment)) return  false;

            GetDatabaseConnection().RegisterSetting(environment);
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