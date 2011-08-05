using System;

namespace Informedica.GenForm.Library.DomainModel.Identification
{
    public static class IdentityComparer
    {
        public static Boolean Compare<T>(T obj1, T obj2)
        {
            return ((IIdentifiable<Int32>) obj1).Identifier.Equals(((IIdentifiable<Int32>) obj2).Identifier);
        }
    }
}
