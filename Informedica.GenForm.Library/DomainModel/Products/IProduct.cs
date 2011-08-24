using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IProduct
    {
        Guid Id { get; }
        String Name { get; }
        String ProductCode { get; }
        String GenericName { get; }
        Brand Brand { get; }
        Shape Shape { get; }
        UnitValue Quantity { get; }
        Package Package { get; }
        String DisplayName { get; }
        IList<ProductSubstance> Substances { get; }
    }
}
