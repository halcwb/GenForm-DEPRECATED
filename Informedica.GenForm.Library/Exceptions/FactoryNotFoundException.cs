using System;

namespace Informedica.GenForm.Library.Exceptions
{
    public class FactoryNotFoundException : Exception
    {
        public FactoryNotFoundException(string factorytype) : base(factorytype) {}
    }
}