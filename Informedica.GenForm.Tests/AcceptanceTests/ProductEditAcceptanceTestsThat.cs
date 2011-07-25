using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using StructureMap;
using TypeMock.ArrangeActAssert;


namespace Informedica.GenForm.Tests.AcceptanceTests
{
    /// <summary>
    /// Summary description for ProductEditAcceptanceTests
    /// </summary>
    [TestClass]
    public class ProductEditAcceptanceTestsThat
    {
        private const String Penicilline = "penicilline";
        private const String Sintrom = "Sintrom";
        private TestContext testContextInstance;
        private const  String Mmol = "Mmol";
        private const String Ampul = "Ampul";
        private const String Tablet = "tablet";

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
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void User_can_start_with_a_new_empty_product()
        {
            var services = GetProductServices();
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(services.GetEmptyProduct()), "User could not start with empty product");
        }

        private static IProductServices GetProductServices()
        {
            return ObjectFactory.GetInstance<IProductServices>();
        }

        [TestMethod]
        public void User_can_change_fields_of_empty_product()
        {
            const string testName = "Test";
            var product = GetProductServices().GetEmptyProduct();
            product.ProductName = testName;
            Assert.AreEqual(testName, product.ProductName,"User could not change the fields of an empty product");
        }


        [Isolated]
        [TestMethod]
        public void When_user_saves_valid_product_no_error_is_thrown()
        {
            var product = GetValidProduct();
            var result = GetProductController().SaveProduct(CreateJObjectFrom(product));

            Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "Save product returned an error ");
        }

        private JObject CreateJObjectFrom(Object product)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(product);
            var jobj = JObject.Parse(json);

            return jobj;

        }

        private ProductController GetProductController()
        {
            return ObjectFactory.GetInstance<ProductController>();
        }

        [Isolated]
        [TestMethod]
        public void When_user_saves_invalid_product_an_error_is_thrown()
        {
            var product = GetInvalidProduct();

            try
            {
                GetProductController().SaveProduct(CreateJObjectFrom(product));
                Assert.Fail("Saving an invalid product should throw an exception");
            }
// ReSharper disable EmptyGeneralCatchClause
            catch(Exception)
// ReSharper restore EmptyGeneralCatchClause
            {
                
            }
        }

        private IProduct GetInvalidProduct()
        {
            var product = GetValidProduct();
            product.PackageName = "";
            return product;
        }

        private IProduct GetValidProduct()
        {
            var product = GetProductServices().GetEmptyProduct();

            product.ProductName = "paracetamol 500 mg tablet";
            product.GenericName = "paracetamol";
            product.BrandName = "Paracetamol";
            product.ShapeName = "tablet";
            product.Quantity = 1;
            product.UnitName = "stuk";
            product.PackageName = "tablet";
            return product;
        }

        [Isolated]
        [TestMethod]
        public void User_cannot_save_product_with_mandatory_fields_not_filled_in()
        {
            var product = GetProductServices().GetEmptyProduct();
            product.ProductName = "Test";

            var result = GetProductController().SaveProduct(CreateJObjectFrom(product));
            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "a non valid product should not be saved");

        }

        [TestMethod]
        public void UserCanAddNewBrandName()
        {
            var brand = CreateNewBrand(Sintrom);
            try
            {
                var result = GetProductController().AddNewBrand(CreateJObjectFrom(brand));

                Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "New brand " + Sintrom + " could not be added");

            }
            catch (Exception e)
            {
                Assert.Fail("an error was throw: " + e);
            }
        }

        [TestMethod]
        public void User_can_add_new_generic_name()
        {
            var generic = CreateNewGeneric(Penicilline);
            try
            {
                var result = GetProductController().AddNewGeneric(CreateJObjectFrom(generic));

                Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "New generic " + Penicilline + " could not be added");

            }
            catch (Exception e)
            {
                Assert.Fail("an error was thrown: " + e);
            }
        }

        private IGeneric CreateNewGeneric(string name)
        {
            var generic = ObjectFactory.GetInstance<IGeneric>();
            generic.GenericName = name;

            return generic;
        }

        [TestMethod]
        public void User_can_add_new_shape_name()
        {
            try
            {
                var shape = CreateNewShape(Tablet);
                var result = GetProductController().AddNewShape(CreateJObjectFrom(shape));

                Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "New shape" + Tablet + " could not be added");

            }
            catch (Exception e)
            {
                Assert.Fail("an error was thrown: " + e);
            }
        }

        private IShape CreateNewShape(String name)
        {
            var shape = ObjectFactory.GetInstance<IShape>();
            shape.ShapeName = name;

            return shape;
        }

        [TestMethod]
        public void User_can_add_new_package_name()
        {
            try
            {
                var package = CreateNewPackage(Ampul);
                var result = GetProductController().AddNewPackage(CreateJObjectFrom(package));

                Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "New package " + Ampul + " could not be added");
            }
            catch (Exception e)
            {
                Assert.Fail("an error was thrown: " + e);
            }
        }

        private IPackage CreateNewPackage(String name)
        {
            var package = ObjectFactory.GetInstance<IPackage>();
            package.PackageName = name;

            return package;
        }

        [TestMethod]
        public void User_can_add_new_unit_name()
        {
            try
            {
                var unit = CreateNewUnit(Mmol);
                var result = GetProductController().AddNewUnit(CreateJObjectFrom(unit));

                Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "New unit: " + Mmol + " could not be added");
            }
            catch (Exception e)
            {
                Assert.Fail("an error was thrown: " + e);
            }
        }

        private IUnit CreateNewUnit(string name)
        {
            var unit = ObjectFactory.GetInstance<IUnit>();
            unit.UnitName = name;

            return unit;
        }

        [TestMethod]
        public void User_cannot_add_invalid_empty_brand()
        {
            var brand = CreateNewBrand(String.Empty);
            var result = GetProductController().AddNewBrand(CreateJObjectFrom(brand));

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "brand with empty name can not be added");
        }

        private IBrand CreateNewBrand(String name)
        {
            var brand = ObjectFactory.GetInstance<IBrand>();
            brand.BrandName = name;

            return brand;
        }
    }
}
