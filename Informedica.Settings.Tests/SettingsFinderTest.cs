using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.Settings.Tests
{
    
    
    /// <summary>
    ///This is a test class for SettingsFinderTest and is intended
    ///to contain all SettingsFinderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SettingsFinderTest
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
        ///A test for FindPath
        ///</summary>
        [TestMethod()]
        public void FindPathOfSettingsFilterShouldReturnFiles()
        {
            var file = "GenFormSettings.xml";
            FileFinder.Filter = new List<string> { @"C:\Users\halcwb\Documents\Visual Studio 2010\Projects\GenForm\" };
            var found = FileFinder.FindPath(file);

            Assert.IsTrue(found.Count() > 0);
        }
    }
}
