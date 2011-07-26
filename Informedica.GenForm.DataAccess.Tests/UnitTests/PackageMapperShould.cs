﻿using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Tests.TestBase;
using Informedica.GenForm.Library.DomainModel.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Package = Informedica.GenForm.Database.Package;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests
{
    /// <summary>
    /// Summary description for PackageMapperShould
    /// </summary>
    [TestClass]
    public class PackageMapperShould: DataMapperTestBase<PackageMapper,IPackage,Package>
    {
        private const string PackageName = "ampul";

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
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
        public void MapPackageToDao()
        {
            Bo.PackageName = PackageName;
            Mapper.MapFromBoToDao(Bo, Dao);
            AssertIsMapped();
        }

        [TestMethod]
        public void MapDaoToPackage()
        {
            Dao.PackageName = PackageName;
            Mapper.MapFromDaoToBo(Dao, Bo);
            AssertIsMapped();
        }

        #region Overrides of DataMapperTestBase<PackageMapper,IPackage,Package>

        protected override bool IsMapped(IPackage bo, Package dao)
        {
            return bo.PackageName == dao.PackageName;
        }

        #endregion
    }
}