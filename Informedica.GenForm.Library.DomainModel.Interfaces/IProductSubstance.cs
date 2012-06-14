using System;

namespace Informedica.GenForm.DomainModel.Interfaces
{
    public interface IProductSubstance
    {
        Guid Id { get;  }
        String Name { get; }
        Int32 SortOrder { get;  }
        ISubstance Substance { get; }
        IUnitValue Quantity { get; }
    }
}