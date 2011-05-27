using StructureMap;

namespace Informedica.GenForm.IoC
{
    public static class LibraryRegistry
    {

        public static void BootstrapStructureMap()
        {

            // Initialize the static ObjectFactory container
            // does not do anything yet, container has no references
            // libraries depend on container not vica versa.

            ObjectFactory.Initialize(x =>
            {

            });

        }

        public static void RegisterImplementationFor<T>(T implementation)
        {
            ObjectFactory.Configure(x => x.For<T>().Use(implementation));
        }

        public static void RegisterTypeFor<T, TC>() where TC: T
        {
            ObjectFactory.Configure(x => x.For<T>().Use<TC>());
        }

        public static T GetImplementationFor<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }


    }
}
