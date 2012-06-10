using System;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Mvc3.Environments;
using Informedica.GenForm.Services;
using StructureMap;
using TypeMock.ArrangeActAssert;
using System.Web.Mvc;
using Informedica.GenForm.Mvc3.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    [TestClass]
    public class InjectingActionInvokerShould
    {
        private InjectingActionInvoker _invoker;
        private ControllerContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = Isolate.Fake.Instance<ControllerContext>();
            var controller = new TestController();

            Isolate.WhenCalled(() => _context.Controller).WillReturn(controller);
            _invoker = ObjectFactory.GetInstance<InjectingActionInvoker>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ObjectFactory.Initialize(x => {});
            System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
        }

        [Isolated]
        [TestMethod]
        public void HaveTestActionFilterWithAnInjectedTestImplementationForITestInterface()
        {
            FilterAttributeDependencyInversionConfigurator.Configure<TestAttribute>();

            var testSetting = new TestImplementation();
            ObjectFactory.Configure(x => x.For<ITestInterface>().Use(testSetting));

            var methodName = "Test";
            Assert.IsTrue(_invoker.InvokeAction(_context, methodName));
        }

        [Isolated]
        [TestMethod]
        public void InjectADatabaseServicesDependencyIntoNHibernateSessionAttribute()
        {
            FilterAttributeDependencyInversionConfigurator.Configure<IDatabaseServices>();

            SetupDatabaseServices();

            InvokeTestNhibernateSessionAttribute();
        }

        private static void SetupDatabaseServices()
        {
            ObjectFactory.Configure(x => x.For<IDatabaseServices>().Use<DatabaseServices>());
            var cache = Isolate.Fake.Instance<HttpSessionCache>();
            ObjectFactory.Configure(x => x.For<ISessionCache>().Use(cache));
        }

        private void InvokeTestNhibernateSessionAttribute()
        {
            var methodName = "TestNHibernateSessionAttribute";
            Assert.IsTrue(_invoker.InvokeAction(_context, methodName));
        }

        [Isolated]
        [TestMethod]
        public void NotBeAbleToInjectDependencyWhenConfiguredWithTypeInWrongNameSpace()
        {
            System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
            FilterAttributeDependencyInversionConfigurator.Configure<TestAttribute>();

            SetupDatabaseServices();

            try
            {
                InvokeTestNhibernateSessionAttribute();
                Assert.Fail("Should not work");

            }
            catch (Exception e)
            {
                 Assert.IsNotInstanceOfType(e, typeof(AssertFailedException));
            }
        }
    }

    public class TestImplementation: ITestInterface
    {
    }

    public class TestController : Controller
    {
        private ViewResult _view;

        public TestController()
        {
            _view = Isolate.Fake.Instance<ViewResult>();
        }

        [Test]
        public ActionResult Test()
        {
            return _view;
        }

        [NHibernateSession]
        public ActionResult TestNHibernateSessionAttribute()
        {
            return _view;
        }
    }

    public class TestAttribute : ActionFilterAttribute
    {
        public ITestInterface Setting { get; set; }
    }


    public interface ITestInterface
    {
    }

}
