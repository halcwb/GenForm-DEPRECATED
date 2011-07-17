using System;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Library.Tests.UnityTests
{
    /// <summary>
    /// Summary description for DatabaseConnectionShould
    /// </summary>
    [TestClass]
    public class DatabaseConnectionShould
    {
        private TestContext testContextInstance;
        private static IDatabaseConnection _databaseConnection;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            ObjectFactory.Inject<IDatabaseConnection>(new DatabaseConnection());

            _databaseConnection = ObjectFactory.GetInstance<IDatabaseConnection>();
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ReturnFalseWhenConnectionStringCannotConnectToDatabase()
        {
            var connectionString =
                @"Data Source=HAL-WIN7\INFORMEDICA;Initial Catalog=Bogus;Integrated Security=True";
            Assert.IsFalse(_databaseConnection.TestConnection(connectionString), "Using connection: " + connectionString + " test connection should return false");
        }

        [TestMethod] 
        public void ReturnTrueWhenConnectectionStringCanConnectToDatabase()
        {
            var connectionString =
                @"Data Source=HAL-WIN7\INFORMEDICA;Initial Catalog=GenForm;Integrated Security=True";
            Assert.IsTrue(_databaseConnection.TestConnection(connectionString), "Using connection: " + connectionString + " test connection should return true");
            
        }
    }

}
