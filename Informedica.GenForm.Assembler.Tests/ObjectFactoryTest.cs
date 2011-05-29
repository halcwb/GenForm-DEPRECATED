﻿using Informedica.GenForm.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using StructureMap.Exceptions;

namespace Informedica.GenForm.Assembler.Tests
{
    
    
    /// <summary>
    ///This is a test class for ObjectFactoryTest and is intended
    ///to contain all ObjectFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObjectFactoryTest
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
        ///A test for GetImplementationFor
        ///</summary>
        public void GetImplementationForTestHelper<T>()
        {
            try
            {
                ObjectFactory.GetInstanceFor<T>();
                Assert.Fail("a non registered type cannot be retrieved");

            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(MissingPluginFamilyException))
                {
                    Assert.Fail("not the expected exception: " + e);
                }
            }
        }

        [TestMethod()]
        public void Get_implementation_for_a_type_that_is_not_registered_returns_exception()
        {
            GetImplementationForTestHelper<GenericParameterHelper>();
        }
    }
}
