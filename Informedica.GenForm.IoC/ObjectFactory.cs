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
                x.AddRegistry(DatabaseRegistry.Instance);
            });

        }

        public static T GetInstanceFor<T>()
        {
            return StructureMap.ObjectFactory.GetInstance<T>();
        }

        public static void InjectInstanceFor<T>(T instance)
        {
            StructureMap.ObjectFactory.Container.Inject(instance);
        }

        public static StructureMap.ExplicitArgsExpression With<T>(T parameter)
        {
            return StructureMap.ObjectFactory.With(parameter);
        }

    }
}
