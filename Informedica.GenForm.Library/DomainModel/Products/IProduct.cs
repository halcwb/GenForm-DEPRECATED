using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IProduct
    {
        Int32 ProductId { get; set; }
        String ProductName { get; set; }
        String ProductCode { get; set; }
        String GenericName { get; set; }
        String BrandName { get; set; }
        String ShapeName { get; set; }
        Decimal Quantity { get; set; }
        String UnitName { get; set; }
        String PackageName { get; set; }
        String DisplayName { get; set; }
        IEnumerable<IProductSubstance> Substances { get; }
    }
}
