using Informedica.GenForm.ServiceProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using IServiceProvider = Informedica.GenForm.ServiceProviders.IServiceProvider;

namespace Informedica.GenForm.Services.Tests
{
    
    
    /// <summary>
    ///This is a test class for ServiceProviderTest and is intended
    ///to contain all ServiceProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ServiceProviderTest
    {

        private static GenericParameterHelper _registeredInstance;
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
        ///A test for Resolve
        ///</summary>
        public void ResolveTestHelper<T>()
        {
            IServiceProvider target = GetServiceProvider();
            if (target == null) target = CreateServiceProvider();

            GenericParameterHelper expected = GetRegisteredInstance(); // TODO: Initialize to an appropriate value
            T actual;
            actual = target.Resolve<T>();
            Assert.AreEqual(expected, actual);
        }

        private static GenericParameterHelper GetRegisteredInstance()
        {
            if (_registeredInstance == null) _registeredInstance =  new GenericParameterHelper();
            return _registeredInstance;
        }

        internal virtual IServiceProvider CreateServiceProvider()
        {
            // TODO: Instantiate an appropriate concrete class.
            ServiceProvider target = Isolate.Fake.Instance<ServiceProvider>();
            Isolate.WhenCalled(() => target.Resolve<GenericParameterHelper>()).WillReturn(GetRegisteredInstance());
            return target;
        }

        internal IServiceProvider GetServiceProvider()
        {
            IServiceProvider provider = ConcreteServiceProvider.Provider;
            Isolate.WhenCalled(() => provider.Resolve<GenericParameterHelper>()).WillReturn(GetRegisteredInstance());
            return provider;
        }

        [Isolated]
        [TestMethod]
        public void A_server_provider_can_resolve_by_type()
        {
            ResolveTestHelper<GenericParameterHelper>();
        }

        [Isolated]
        [TestMethod]
        public void Service_provider_returns_two_seperate_instances()
        {
            IServiceProvider provider1 = GetServiceProvider();
            IServiceProvider provider2 = ConcreteServiceProvider2.Provider;

            Assert.IsNotNull(provider1.Resolve <GenericParameterHelper>());
            Assert.IsNull(provider2.Resolve<GenericParameterHelper>());
        }
    }
}
