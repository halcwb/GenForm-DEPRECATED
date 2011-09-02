using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Interfaces
{
    public interface IProductSubstance
    {
        Guid Id { get;  }
        String Name { get; }
        Int32 SortOrder { get;  }
        ISubstance Substance { get; }
        UnitValue Quantity { get; }
    }
}