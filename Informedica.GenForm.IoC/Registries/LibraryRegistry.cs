using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.IoC.Registries
{
    public static class LibraryRegistry
    {
        private static Registry _libraryRegistry = new Registry();

        public static Registry Instance { get { return _libraryRegistry; } }

        public static void RegisterInstanceFor<T>(T instance)
        {
            _libraryRegistry.For<T>().Use(instance);
        }

        public static void RegisterTypeFor<T, TC>() where TC: T
        {
            _libraryRegistry.For<T>().Use<TC>();
        }


    }
}
