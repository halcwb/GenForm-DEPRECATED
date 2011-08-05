using System;

namespace Informedica.GenForm.Library.DomainModel.Identification
{
    public interface IEquals<T>
    {
        Boolean Equals(T item);
    }
}