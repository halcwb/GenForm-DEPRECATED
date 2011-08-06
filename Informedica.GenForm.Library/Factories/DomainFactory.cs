namespace Informedica.GenForm.Library.Factories
{
    public static class DomainFactory
    {
        public static T Create<T, TC>(TC dto)
        {
            return Factory.ObjectFactory.Instance.With(dto).GetInstance<T>();
        }
    }
}