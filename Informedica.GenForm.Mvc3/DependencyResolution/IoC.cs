using System.Web;
using System.Web.Mvc;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Services;
using StructureMap;

namespace Informedica.GenForm.Mvc3
{

    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });

                            x.For<IControllerActivator>().Use<StructureMapControllerActivator>();
                            x.For<IDatabaseServices>().Use<DatabaseServices>();

                            x.For<ISessionCache>().AlwaysUnique().Use<HttpSessionCache>();
                            x.For<HttpSessionStateBase>().AlwaysUnique().Use(s => new HttpSessionStateWrapper(HttpContext.Current.Session));

                        });

            return ObjectFactory.Container;
        }
    }
}