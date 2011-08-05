using System;

namespace Informedica.GenForm.Library.DomainModel.Identification
{
    public interface IIdentifier<T>
    {
        T Id { get; }
        String Name { get; }
        Boolean Equals(IIdentifier<T> identifier);
    }
}