using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Interfaces
{
    public interface IProductSubstance
    {
        Guid Id { get;  }
        Int32 SortOrder { get;  }
        Substance Substance { get; }
        UnitValue Quantity { get; }
    }
}