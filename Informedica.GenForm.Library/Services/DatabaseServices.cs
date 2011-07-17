using System;
using Informedica.GenForm.Library.DomainModel.Databases;
using StructureMap;

namespace Informedica.GenForm.Library.Services
{
    public class DatabaseServices : IDatabaseServices
    {
        #region Implementation of IDatabaseServices

        public bool TestDatabaseConnection(IDatabaseSetting databaseSetting)
        {
            return GetDatabaseConnection().TestConnection(databaseSetting.ConnectionString);
        }

        public bool RegisterDatabaseSetting(IDatabaseSetting databaseSetting)
        {
            if(!TestDatabaseConnection(databaseSetting)) return  false;

            GetDatabaseConnection().RegisterSetting(databaseSetting);
            return true;
        }

        private IDatabaseConnection GetDatabaseConnection()
        {
            return ObjectFactory.GetInstance<IDatabaseConnection>();
        }

        #endregion
    }
}