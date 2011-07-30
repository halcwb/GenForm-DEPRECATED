using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products.dto;
using StructureMap;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    public abstract class ProductTestBase
    {
        protected const string ProductName = "dopamine Dynatra infusievloeistof 5 mL ampul";
        protected const string BrandName = "Dynatra";
        protected const string GenericName = "dopamine";
        protected const string Shape = "infusievloeistof";
        protected const string Pacakage = "ampul";
        protected const string Unit = "mL";
        protected const string DisplayName = "dopamine Dynatra infusievloeistof 5 mL ampul";
        protected const Decimal Quantity = 5;
        protected const String ProductCode = "1";

        protected static IProduct GetProduct(ProductDto dto)
        {
            return ObjectFactory.With(dto).GetInstance<IProduct>();
        }
    }
}
