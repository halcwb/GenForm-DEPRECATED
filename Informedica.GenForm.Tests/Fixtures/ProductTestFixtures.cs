using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.Library.Services.Products.dto;

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
                Id = 0,
                ProductName = ProductName,
                Brand = Brand,
                DisplayName = DisplayName,
                Generic = Generic,
                Package = Package,
                ProductCode = ProductCode,
                Quantity = ProductQuantity,
                Shape = Shape,
                Unit = ProductUnit
            };


        }

        public static ProductDto GetProductDtoWithOneSubstance()
        {
            var dto = GetProductDtoWithNoSubstances();
            dto.Substances = new List<SubstanceDto>
                                 {
                                     new SubstanceDto
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
            dto.Substances = new List<SubstanceDto>
                                 {
                                     dto.Substances.First(),
                                     new SubstanceDto
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
            dto.Routes = new List<RouteDto> {new RouteDto {Id = 0, Route = Route}};
            return dto;
        }
    }
}
