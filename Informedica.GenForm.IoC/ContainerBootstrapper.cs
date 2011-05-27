using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services;
using StructureMap;

namespace Informedica.GenForm.IoC
{
    public static class ContainerBootstrapper
    {

        public static void BootstrapStructureMap()
        {

            // Initialize the static ObjectFactory container

            ObjectFactory.Initialize(x =>
            {

                x.For<ILoginServices>().Use<LoginServices>();
                x.For<IProductServices>().Use<ProductServices>();

            });

        }

        public static void RegisterInstanceOfType<T>(T instance)
        {
            ObjectFactory.Configure(x => x.For<T>().Use(instance));
        }

        public static void RegisterType<T, TC>() where TC: T
        {
            ObjectFactory.Configure(x => x.For<T>().Use<TC>());
        }

        public static T GetInstanceOfType<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }


    }
}
