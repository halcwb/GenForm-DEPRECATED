using System.Collections.Generic;
using Informedica.Factory;

namespace Informedica.GenForm.Library.Factories
{
    public static class HashSetFactory
    {
        public static HashSet<T> Create<T>()
        {
            return new HashSet<T>(CreateComparer<T>());
        }

        private static IEqualityComparer<T> CreateComparer<T>()
        {
            return ObjectFactory.Instance.GetInstance<IEqualityComparer<T>>();
        }
    }
}
