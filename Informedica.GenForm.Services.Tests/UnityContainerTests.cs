using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Services.Tests
{
    /// <summary>
    /// Summary description for UnityContainerTests
    /// </summary>
    [TestClass]
    public class UnityContainerTests
    {
        public UnityContainerTests()
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
        public void Resolution_of_inherited_interface()
        {
            var testObject = (ITestInterface)new TestObject();
            IUnityContainer container = new UnityContainer();
            container.RegisterInstance(testObject);

            Assert.IsInstanceOfType(testObject, typeof(ITestInterface), "Testobject should be of type ITestInterface");
            try
            {
                container.Resolve<ITestInterface>();
            }
            catch (Exception)
            {
                Assert.Fail("ITestInterface could not be resolved");   
            }
        }

        [TestMethod]
        public void Try_to_find_generic_typed_interface_without_specifying_type()
        {
            var testclass = (IInterface<TestClass, String>)new TestClass();
            IUnityContainer container = new UnityContainer();
            container.RegisterInstance("test", testclass);
            
           
            Assert.IsNotNull(container.Registrations.First(r => r.Name == "test"));
        }

        public class TestObject: ITestInterface
        {
            
        }

        public interface ITestInterface
        {
            
        }

        public interface IInterface<T,TC>
        {
            
        }

        public class TestClass: IInterface<TestClass, String>
        {
            
        }
    }
}
