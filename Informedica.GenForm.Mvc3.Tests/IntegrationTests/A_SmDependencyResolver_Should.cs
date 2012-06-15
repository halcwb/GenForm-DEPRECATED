using System;
using System.Web;
using System.Web.Mvc;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.GenForm.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Mvc3.Tests.IntegrationTests
{
    [TestClass]
    public class A_SmDependencyResolver_Should
    {
        private IContainer _container;

        [TestInitialize]
        public void Setup()
        {
            ObjectFactory.Configure(x => x.For<IDatabaseServices>().Use<DatabaseServices>());
            ObjectFactory.Configure(x => x.For<ISessionCache>().Use<HttpSessionCache>());

            var stateBase = Isolate.Fake.Instance<HttpSessionStateBase>();
            ObjectFactory.Configure(x => x.For<HttpSessionStateBase>().Use(stateBase));
            _container = ObjectFactory.Container;
        }

        [Isolated]
        [TestMethod]
        public void call_objectfactory_container_getinstance_to_get_a_LoginController()
        {
            _container = Isolate.Fake.Instance<IContainer>();
            DependencyResolver.SetResolver(new SmDependencyResolver(_container));            

            DependencyResolver.Current.GetService<LoginController>();
            Isolate.Verify.WasCalledWithAnyArguments(() => _container.GetInstance(typeof(LoginController)));
        }

        [TestMethod]
        public void be_able_to_create_a_login_controller()
        {
            DependencyResolver.SetResolver(new SmDependencyResolver(_container));

            var controller = DependencyResolver.Current.GetService<LoginController>();

            Assert.IsInstanceOfType(controller, typeof(LoginController));
        }
    }
}
