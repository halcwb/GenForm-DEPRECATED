using System;

namespace Informedica.GenForm.Library.Exceptions
{
    public class DuplicateEntityException<T> : Exception
    {
        public DuplicateEntityException(T item): base(item.ToString()) {}
    }
}