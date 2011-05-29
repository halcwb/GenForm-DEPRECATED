using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.IoC.Registries
{
    public static class DatabaseRegistry
    {
        private static Registry _databaseRegistry = new Registry();

        public static Registry Instance { get { return _databaseRegistry; } }

        public static void RegisterInstanceFor<T>(T instance)
        {
            _databaseRegistry.For<T>().Use(instance);
        }

        public static void RegisterTypeFor<T, TC>() where TC:T
        {
            _databaseRegistry.For<T>().Use<TC>();
        }


    }
}
