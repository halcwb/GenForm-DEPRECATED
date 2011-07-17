using System;
using Informedica.GenForm.Library.DomainModel.Databases;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class DatabaseConnection: IDatabaseConnection
    {
        public enum DatabaseName 
        {
            Formularium2010,
            Genpres, 
            GenForm
        }

        public static string GetConnectionString(DatabaseName database) 
        {
            String connection;

            switch (database){
                case DatabaseName.Formularium2010:
                    try
                    {
                        connection = System.Configuration.ConfigurationManager.ConnectionStrings[GetConnectionName(database)].ConnectionString;
                    }
                    catch (Exception)
                    {
                        connection = "";
                    }
                    break;
                case DatabaseName.GenForm:
                    try
                    {
                        //connection = @"Data Source=INDURAIN;Initial Catalog=GenForm;User ID=genform; Password=genform";
                        connection = @"Data Source=HAL-WIN7\INFORMEDICA;Initial Catalog=GenForm;Integrated Security=True";
                    }
                    catch (Exception)
                    {
                        // connection = @"Data Source=INDURAIN;Initial Catalog=GenForm;User ID=genform; Password=genform";
                        connection = @"Data Source=HAL-WIN7\INFORMEDICA;Initial Catalog=GenForm;Integrated Security=True";
                    }
                    break;
                default:
                    throw new Exception("Database not found");
            }

            return connection;
            
        }

        private static string GetConnectionName(DatabaseName database) 
        {
            return (GetComputerName() + "_" + Enum.GetName(typeof(DatabaseName), database));
        }


        public static string GetComputerName()
        {
            return Environment.MachineName;
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

        public void RegisterSetting(IDatabaseSetting databaseSetting)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
