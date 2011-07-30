﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.Services.Products.dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for NewlyCreatedProductShould
    /// </summary>
    [TestClass]
    public class NewlyCreatedProductShould: ProductTestBase
    {
        public NewlyCreatedProductShould()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize(); }
        
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
        public void NotBeChanchableByItsDto()
        {
            var dto = new ProductDto {ProductName = ProductName};
            var product = GetProduct(dto);
            dto.ProductName = "Cannot be changed";
            Assert.IsTrue(product.ProductName == ProductName, "product name was changed");
        }
    }
}
