﻿using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Databases;
using Informedica.GenForm.Settings;

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

        public static string GetLocalConnectionString(DatabaseName databaseName)
        {
            return @"Data Source=hal-win7\informedica;Initial Catalog=GenFormTest;Integrated Security=True;Connect TimeOut=0.5";
        }


        public Boolean TestConnection(String connectionString)
        {
            connectionString += ";Connect TimeOut=0.5";
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
            var connectionString =  SettingsManager.Instance.ReadSecureSetting(name);
            return connectionString;
        }

        public IEnumerable<string> GetDatabases()
        {
            throw new NotImplementedException();
        }

    }
}
