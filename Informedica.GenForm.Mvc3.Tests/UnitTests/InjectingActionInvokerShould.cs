using System.Web.Routing;
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

        [Isolated]
        [TestMethod]
        public void HaveTestActionFilterWithAnInjectedTestImplementationForITestInterface()
        {
            ObjectFactory.Configure(x =>
                                        {
                                            x.For<IActionInvoker>().Use<InjectingActionInvoker>();
                                            x.For<ITempDataProvider>().Use<SessionStateTempDataProvider>();
                                            x.For<RouteCollection>().Use(RouteTable.Routes);

                                            x.SetAllProperties(c =>
                                                                   {
                                                                       c.OfType<IActionInvoker>();
                                                                       c.OfType<ITempDataProvider>();
                                                                       c.WithAnyTypeFromNamespaceContainingType
                                                                           <TestAttribute>();

                                                                   });
                                        });

            var testSetting = new TestImplementation();
            ObjectFactory.Configure(x => x.For<ITestInterface>().Use(testSetting));


            var context = Isolate.Fake.Instance<ControllerContext>();
            var testController = new TestController();

            Isolate.WhenCalled(() => context.Controller).WillReturn(testController);
            var invoker = ObjectFactory.GetInstance<InjectingActionInvoker>();


            Assert.IsTrue(invoker.InvokeAction(context,"Test"));
        }
    }

    public class TestImplementation: ITestInterface
    {
    }

    public class TestController : Controller
    {
        [TestAttribute]
        public ActionResult Test()
        {
            var view = Isolate.Fake.Instance<ViewResult>();
            return view;
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
