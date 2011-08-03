using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface ISubstance
    {
        Int32 SubstanceId { get; set; }
        String SubstanceName { get; set; }
    }
}