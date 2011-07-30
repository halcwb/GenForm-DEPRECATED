using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IProductSubstance
    {
        Int32 SortOrder { get; set; }
        String Substance { get; set; }
        Decimal Quantity { get; set; }
        String Unit { get; set; }
    }
}