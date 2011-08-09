using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Services.Products;
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
    public class SaveNewProductTests
    {
        private const String Penicilline = "penicilline";
        private const String Sintrom = "Sintrom";
        private TestContext testContextInstance;
        private const String Mmol = "Mmol";
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
        public void ThatUserCanStartWithAnEmptyProduct()
        {
            var services = GetProductServices();
            Assert.IsTrue(ObjectExaminer.ObjectHasEmptyProperties(services.GetEmptyProduct()), "User could not start with empty product");
        }

        private static IProductServices GetProductServices()
        {
            return ObjectFactory.GetInstance<IProductServices>();
        }

        [Isolated]
        [TestMethod]
        public void ThatNoErrorIsThrownWhenUserSavesAValidProduct()
        {
            var product = GetValidProduct();

            var repos = ObjectFactory.GetInstance<IRepository<IProduct>>();
            ObjectFactory.Inject(typeof(IRepository<IProduct>), repos);

            using (repos.Rollback)
            {
                var result = GetProductController().SaveProduct(product);

                Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "Save product returned an error " +
                              ActionResultParser.GetPropertyValue<String>(result, "message"));

            }
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
        public void ThatAnErrorIsThrownWhenUserSavesAnInvalidProduct()
        {
            var product = GetInvalidProduct();

            try
            {
                GetProductController().SaveProduct(product);
                Assert.Fail("Saving an invalid product should throw an exception");
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {

            }
        }

        private static ProductDto GetInvalidProduct()
        {
            var product = GetValidProduct();
            product.ShapeName = "";
            return product;
        }

        private static ProductDto GetValidProduct()
        {
            return new ProductDto
            {
                ProductName = "dopamine Dynatra infusievloeistof 200 mg 5 mL ampul",
                DisplayName = "dopamine Dynatra infusievloeistof 200 mg 5 mL ampul",
                GenericName = "dopamine",
                BrandName = "Dynatra",
                ShapeName = "infusievloeistof",
                Quantity = 5,
                UnitName = "mL",
                PackageName = "ampul"
            };

        }

        [Isolated]
        [TestMethod]
        public void ThatUserCannotSaveProductWithMandatoryFieldsNotFilledIn()
        {
            var product = new ProductDto();
            product.ProductName = "Test";

            var result = GetProductController().SaveProduct(product);
            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "a non valid product should not be saved");

        }

        [TestMethod]
        public void UserCanAddNewBrandName()
        {
            var brand = CreateNewBrand(Sintrom);
            try
            {
                var repos = ObjectFactory.GetInstance<IRepository<IBrand>>();
                ObjectFactory.Inject(typeof(IRepository<IBrand>), repos);

                using (repos.Rollback)
                {
                    var result = GetProductController().AddNewBrand(CreateJObjectFrom(brand));

                    Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "New brand " + Sintrom + " could not be added");
                }
            }
            catch (Exception e)
            {
                Assert.Fail("an error was throw: " + e);
            }
        }

        [TestMethod]
        public void UserCanAddANewGenericName()
        {
            var generic = CreateNewGeneric(Penicilline);
            try
            {
                var repos = ObjectFactory.GetInstance<IRepository<IGeneric>>();
                ObjectFactory.Inject(typeof(IRepository<IGeneric>), repos);

                using (repos.Rollback)
                {
                    var result = GetProductController().AddNewGeneric(CreateJObjectFrom(generic));

                    Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "New generic " + Penicilline + " could not be added");
                }
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
        public void UserCanAddNewShapeName()
        {
            try
            {
                var shape = CreateNewShape(Tablet);
                var repos = ObjectFactory.GetInstance<IRepository<IShape>>();
                ObjectFactory.Inject(typeof(IRepository<IShape>), repos);
                using (repos.Rollback)
                {
                    var result = GetProductController().AddNewShape(CreateJObjectFrom(shape));

                    Assert.IsTrue(ActionResultParser.GetSuccessValue(result), "New shape" + Tablet + " could not be added");
                }
            }
            catch (Exception e)
            {
                Assert.Fail("an error was thrown: " + e);
            }
        }

        private IShape CreateNewShape(String name)
        {
            var shape = DomainFactory.Create<IShape, ShapeDto>(new ShapeDto{ Name = name});

            return shape;
        }

        [TestMethod]
        public void UserCanAddNewPackageName()
        {
            try
            {
                var package = CreateNewPackage(Ampul);
                var repos = ObjectFactory.GetInstance<IRepository<IPackage>>();
                ObjectFactory.Inject(typeof(IRepository<IPackage>), repos);

                using (repos.Rollback)
                {
                    var result = GetProductController().AddNewPackage(CreateJObjectFrom(package));


                    Assert.IsTrue(ActionResultParser.GetSuccessValue(result),
                                  "New package " + Ampul + " could not be added");
                }
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
        public void UserCanAddNewUnitName()
        {
            try
            {
                var unit = CreateNewUnit(Mmol);

                var repos = ObjectFactory.GetInstance<IRepository<IUnit>>();
                ObjectFactory.Inject(typeof(IRepository<IUnit>), repos);

                using (repos.Rollback)
                {
                    var result = GetProductController().AddNewUnit(CreateJObjectFrom(unit));


                    Assert.IsTrue(ActionResultParser.GetSuccessValue(result),
                                  "New unit: " + Mmol + " could not be added");
                }
            }
            catch (Exception e)
            {
                Assert.Fail("an error was thrown: " + e);
            }
        }

        private IUnit CreateNewUnit(string name)
        {
            var unit = ObjectFactory.GetInstance<IUnit>();
            unit.Name = name;

            return unit;
        }

        [TestMethod]
        public void UserCannotAddBrandWithoutBrandName()
        {
            var brand = CreateNewBrand(String.Empty);
            var result = GetProductController().AddNewBrand(CreateJObjectFrom(brand));

            Assert.IsFalse(ActionResultParser.GetSuccessValue(result), "brand with empty name can not be added");
        }

        private IBrand CreateNewBrand(String name)
        {
            var brand = DomainFactory.Create<IBrand, BrandDto>(new BrandDto { Name = "Dynatra"});

            return brand;
        }
    }
}
