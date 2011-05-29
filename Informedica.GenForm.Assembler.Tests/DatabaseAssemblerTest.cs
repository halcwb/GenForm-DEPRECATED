﻿using Informedica.GenForm.Assembler;
using Informedica.GenForm.Database;
using Informedica.GenForm.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Informedica.GenForm.Assembler.Tests
{
    
    
    /// <summary>
    ///This is a test class for DatabaseAssemblerTest and is intended
    ///to contain all DatabaseAssemblerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DatabaseAssemblerTest
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
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
        [TestMethod()]
        public void After_assembler_has_registerd_a_data_context_can_be_retrieved()
        {
            DatabaseAssembler.RegisterDependencies();
            ObjectFactory.Initialize();
            var connection = DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm);
            var context = ObjectFactory.With<String>(connection).GetInstance<GenFormDataContext>();
            Assert.IsNotNull(context);
        }
    }
}
