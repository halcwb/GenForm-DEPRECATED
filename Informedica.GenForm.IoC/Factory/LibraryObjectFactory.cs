using StructureMap;

namespace Informedica.GenForm.IoC.Factory
{
    public class LibraryObjectFactory
    {
        public static T GetImplementationFor<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }

    }
}
