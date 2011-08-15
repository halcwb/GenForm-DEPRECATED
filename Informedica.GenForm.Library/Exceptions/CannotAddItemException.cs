using System;

namespace Informedica.GenForm.Library.Exceptions
{
    public class CannotAddItemException<T> : Exception
    {
        public CannotAddItemException(T item): base(item.ToString()) {}
    }
}