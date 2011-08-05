using StructureMap;

namespace Informedica.GenForm.Library.Factories
{
    public static class DomainFactory
    {
        public static T Create<T, TC>(TC dto)
        {
            return ObjectFactory.With(dto).GetInstance<T>();
        }
    }
}