using StructureMap;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.IoC.Registries
{
    public static class LibraryRegistry
    {
        private static Registry _libraryRegistry = new Registry();

        public static Registry Instance { get { return _libraryRegistry; } }

        public static void RegisterImplementationFor<T>(T implementation)
        {
            _libraryRegistry.For<T>().Use(implementation);
            // StructureMap.ObjectFactory.Configure(x => x.For<T>().Use(implementation));
        }

        public static void RegisterTypeFor<T, TC>() where TC: T
        {
            _libraryRegistry.For<T>().Use<TC>();
            // StructureMap.ObjectFactory.Configure(x => x.For<T>().Use<TC>());
        }

        //public static T GetImplementationFor<T>()
        //{
        //    return StructureMap.ObjectFactory.GetInstance<T>();
        //}


    }
}
