using System;

namespace Informedica.GenForm.Library.Exceptions
{
    public class CannotRemoveItemException<T> : Exception
    {
        public CannotRemoveItemException(T item) : base(item.ToString()) {}
    }
}