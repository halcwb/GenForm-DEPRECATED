using System;

namespace Informedica.GenForm.Library.DomainModel.Identification
{
    public class Identifier<T,TC>: IIdentifier<T>
    {
        private readonly IdentifierEquals<T> _equals;

        internal Identifier(T id, String name, IdentifierEquals<T> equals)
        {
            Id = id;
            Name = name;
            _equals = equals;
        }

        public T Id { get; private set; }
    
        public String Name { get; private set; }
    
        public bool Equals(IIdentifier<T> identifier)
        {
            return _equals(identifier);
        }
    }
}