using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface ISubstanceGroup
    {
        Guid Id { get; }
        String Name { get; }
        ISubstanceGroup MainSubstanceGroup { get; set; }
    }
}