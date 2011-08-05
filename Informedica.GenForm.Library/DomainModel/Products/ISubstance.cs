using System;
using Informedica.GenForm.Library.DomainModel.Identification;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface ISubstance
    {
        Int32 SubstanceId { get; set; }
        String SubstanceName { get; set; }
    }
}

public interface IIdentifiable<T>
{
    IIdentifier<T> Identifier { get; }    
}

