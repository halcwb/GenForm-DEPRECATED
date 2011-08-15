using System;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Exceptions
{
    public class CannotAddItemException : Exception
    {
        public CannotAddItemException(Unit unit): base(unit.ToString()) {}
    }
}