using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Databases;
using Informedica.Settings;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class DatabaseConnection: IDatabaseConnection
    {
        public enum DatabaseName 
        {
            Formularium2010,
            Genpres, 
            GenForm,
            GenFormTest
        }

        public static string GetConnectionString(DatabaseName database) 
        {
            var instance = new DatabaseConnection();
            return instance.GetConnectionString(Enum.GetName(typeof(DatabaseName), database));
        }

        public static string GetEnvironment(string environment)
        {
            var instance = new DatabaseConnection();
            return instance.GetConnectionString(environment);
        }

        public static string GetLocalConnectionString(DatabaseName databaseName)
        {
            return @"Data Source=hal-win7\informedica;Initial Catalog=GenFormTest;Integrated Security=True";
        }

        #region Implementation of IDatabaseConnection

        public Boolean TestConnection(String connectionString)
        {
            try
            {
                using (System.Data.IDbConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }                            
        }

        public void RegisterSetting(IEnvironment environment)
        {
            SettingsManager.Instance.WriteSecureSetting(environment.Name,
                                                         environment.ConnectionString);
        }

        public string GetConnectionString(String name)
        {
            return SettingsManager.Instance.ReadSecureSetting(name);
        }

        public void SetSettingsPath(string path)
        {
            SettingsManager.Instance.Initialize(path);
        }

        public IEnumerable<string> GetDatabases()
        {
            return SettingsManager.Instance.GetNames();
        }

        #endregion
    }
}
