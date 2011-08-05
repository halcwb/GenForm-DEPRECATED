using Informedica.GenForm.DataAccess.Repositories.Delegates;
using Informedica.GenForm.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using StructureMap;
using StructureMap.Exceptions;

namespace Informedica.GenForm.Assembler.Tests
{
    
    
    /// <summary>
    ///This is a test class for ObjectFactoryTest and is intended
    ///to contain all ObjectFactoryTest Unit Tests
    ///</summary>
    [TestClass]
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
                ObjectFactory.GetInstance<T>();
                Assert.Fail("a non registered type cannot be retrieved");

            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(StructureMapException))
                {
                    Assert.Fail("not the expected exception: " + e);
                }
            }
        }

        [TestMethod]
        public void GetImplementationForNonRegisteredType()
        {
            GetImplementationForTestHelper<IMissingPlugin>();
        }

        [TestMethod]
        public  void ThatADelegateCanBeRegistered()
        {
            InsertOnSubmit<Product> insert = ProductDelegates.InsertOnSubmit;
            ObjectFactory.Inject(insert);
            Assert.IsNotNull(ObjectFactory.GetInstance<InsertOnSubmit<Product>>());
        }


    }

    public interface IMissingPlugin
    {
            
    }
}
