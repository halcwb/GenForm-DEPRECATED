using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class ProductTestFixtures
    {
        public const string ProductName = "dopamine (Dynatra) 200 mg in 5 mL infusievloeistof per ampul";
        public const string DisplayName = "dopamine (Dynatra) 200 mg in 5 mL infusievloeistof per ampul";
        public const string Generic = "dopamine";
        public const string Shape = "infusievloeistof";
        public const string Package = "ampul";
        public const Decimal ProductQuantity = 5;
        public const Decimal SubstanceQuantity = 200;
        public const string ProductUnit = "mL";
        public const string Brand = "Dynatra";
        public const Int32 SortOrder = 1;
        public const String Substance = "dopamine";
        public const String SubstanceUnit = "mg";
        public const string ProductCode = "1";
        public const string Route = "iv";


        public static ProductDto GetProductDtoWithNoSubstances()
        {
            return new ProductDto
            {
                ProductId =  0,
                ProductName = ProductName,
                BrandName = Brand,
                DisplayName = DisplayName,
                GenericName = Generic,
                PackageName = Package,
                ProductCode = ProductCode,
                Quantity = ProductQuantity,
                ShapeName = Shape,
                UnitName = ProductUnit
            };


        }

        public static ProductDto GetProductDtoWithOneSubstance()
        {
            var dto = GetProductDtoWithNoSubstances();
            dto.Substances = new List<ProductSubstanceDto>
                                 {
                                     new ProductSubstanceDto
                                         {
                                             Id = 0, 
                                             Quantity = SubstanceQuantity, 
                                             SortOrder = SortOrder, 
                                             Substance = Substance, 
                                             Unit = SubstanceUnit
                                         }
                                 };
            return dto;
        }

        public static ProductDto GetProductDtoWithTwoSubstances()
        {
            var dto = GetProductDtoWithOneSubstance();
            dto.Substances = new List<ProductSubstanceDto>
                                 {
                                     dto.Substances.First(),
                                     new ProductSubstanceDto
                                         {
                                             Id = 0,
                                             Quantity = 5,
                                             SortOrder = 2,
                                             Substance = "water",
                                             Unit = "mL"
                                         }
                                 };
            return dto;
        }

        public static ProductDto GetProductDtoWithTwoSubstancesAndRoute()
        {
            var dto = GetProductDtoWithTwoSubstances();
            dto.Routes = new List<RouteDto> {new RouteDto {Id = Guid.Empty, Name = Route}};
            return dto;
        }

        public static IEnumerable<ProductDto> GetProductDtoListWithThreeItems()
        {
            var dto = GetProductDtoWithNoSubstances();
            var list = new List<ProductDto>();
            list.Add(dto);
            dto = dto.CloneDto();
            dto.ProductName = "paracetamol Paracetamol zetpil 1 stuk";
            dto.BrandName = "Paracetamol";
            dto.GenericName = "paracetamol";
            dto.PackageName = "zetpil";
            dto.Quantity = 500;
            dto.ShapeName = "zetpil";
            dto.UnitName = "stuk";
            list.Add(dto);
            dto = dto.CloneDto();
            dto.ProductName = "lactulose Duphalac stroop 200 milliliter";
            dto.BrandName = "Duphalac";
            dto.GenericName = "lactulose";
            dto.ShapeName = "stroop";
            dto.Quantity = 200;
            dto.UnitName = "milliliter";
            list.Add(dto);

            return list;
        }
    }
}
