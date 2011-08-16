using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for NewPackageShould
    /// </summary>
    [TestClass]
    public class NewPackageShould
    {
        public NewPackageShould()
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
        public void BeAbleToAssociateWithShape()
        {
            var package = AssociateWithShape();

            Assert.IsTrue(package.Shapes.Count() == 1);
        }

        private static Package AssociateWithShape()
        {
            var package = Package.Create(new PackageDto
                                          {
                                              Name = "ampul"
                                          });
            package.AddShape(Shape.Create(new ShapeDto
                                           {
                                               Name = "infusievloeistof"
                                           }));
            return package;
        }

        [TestMethod]
        public void BeAbleToAssociatePackageWithShape()
        {
            var package = Package.Create(new PackageDto
                                            {
                                                Name = "ampul"
                                            });
            var shape = Shape.Create(new ShapeDto
                                           {
                                               Name = "infusievloeistof"
                                           });
            package.AddShape(shape);

            Assert.IsTrue(shape.Packages.Count() == 1);
            
        }

        [TestMethod]
        public void NotAcceptTwoDuplicateShapes()
        {
            var package = AssociateWithShape();
            try
            {
                package.AddShape(Shape.Create(new ShapeDto
                                       {
                                           Name = "infusievloeistof"
                                       }));
                Assert.Fail(new StackFrame().GetMethod().Name);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(CannotAddItemException<Shape>));
            }

        }
    }
}
