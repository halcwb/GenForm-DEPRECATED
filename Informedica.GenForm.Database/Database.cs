using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Database
{
    public static class DatabaseConnection
    {
        public enum DatabaseName 
        {
            FORMULARIUM2010,
            GENPRES, 
            GenForm
        }

        public static string GetConnectionString(DatabaseName database) 
        {
            string connection = string.Empty;

            switch (database){
                case DatabaseName.FORMULARIUM2010:
                    try
                    {
                        connection = System.Configuration.ConfigurationManager.ConnectionStrings[GetConnectionName(database)].ConnectionString;
                    }
                    catch (Exception e)
                    {
                        // Temporary solution because Linqpad cannot locate app.config
                    }
                    break;
                case DatabaseName.GenForm:
                    try
                    {
                        connection = @"Data Source=HAL-WIN7\INFORMEDICA;Initial Catalog=GenForm;Integrated Security=True";
                    }
                    catch (Exception e)
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
            return System.Environment.MachineName;
        }

    }
}
