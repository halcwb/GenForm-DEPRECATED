﻿using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Assembler.Tests
{   
    /// <summary>
    ///This is a test class for DatabaseAssemblerTest and is intended
    ///to contain all DatabaseAssemblerTest Unit Tests
    ///</summary>
    [TestClass]
    public class DatabaseAssemblerShouldRegister
    {
        private TestContext testContextInstance;

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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for RegisterDependencies
        ///</summary>
        [TestMethod]
        public void AnInstanceOfGenFormDataContext()
        {
            var connection = DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm);
            ObjectFactoryAssertUtility.AssertRegistrationWith<string, GenFormDataContext>(connection);
        }

        [TestMethod]
        public void NotADefaultInstanceOfGenFormDataContext()
        {
            try
            {
                ObjectFactoryAssertUtility.AssertRegistration<GenFormDataContext>("default instance");
                Assert.Fail("should not be able to create context without connection");
            }
            catch (System.Exception e)
            {
                Assert.IsNotNull(e);
            }
        }
    }
}
