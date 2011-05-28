using Informedica.GenForm.IoC.Registries;

namespace Informedica.GenForm.IoC
{
    public static class ObjectFactory
    {
        public static void Initialize()
        {

            StructureMap.ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(LibraryRegistry.Instance);
            });

        }

        public static T GetImplementationFor<T>()
        {
            return StructureMap.ObjectFactory.GetInstance<T>();
        }

    }
}
