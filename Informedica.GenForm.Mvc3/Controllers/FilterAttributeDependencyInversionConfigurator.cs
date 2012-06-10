using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public static class FilterAttributeDependencyInversionConfigurator
    {
        public static void Configure<T>()
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
                                                                           <T>();
                                                                   });
                                        });
        }
    }
}