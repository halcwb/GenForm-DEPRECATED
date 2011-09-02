using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

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
                Name = ProductName,
                BrandName = Brand,
                DisplayName = DisplayName,
                GenericName = Generic,
                PackageName = Package,
                ProductCode = ProductCode,
                Quantity = ProductQuantity,
                ShapeName = Shape,
                UnitName = ProductUnit,
                UnitAbbreviation = "mL",
                UnitDivisor = 1000M,
                UnitGroupAllowConversion = true,
                UnitGroupName = "volume",
                UnitIsReference = false,
                UnitMultiplier = 0.001M
            };


        }

        public static ProductDto GetProductDtoWithOneSubstance()
        {
            var dto = GetProductDtoWithNoSubstances();
            dto.Substances = new List<ProductSubstanceDto>
                                 {
                                     new ProductSubstanceDto
                                         {
                                             Quantity = SubstanceQuantity, 
                                             SortOrder = SortOrder, 
                                             Substance = Substance, 
                                             UnitName = SubstanceUnit,
                                             UnitAbbreviation = "mg",
                                             UnitGroupAllowConversion = true,
                                             UnitGroupName = "massa",
                                             UnitMultiplier = 0.001M,
                                             UnitDivisor = 1000M,
                                             UnitIsReference = false
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
                                             Id = Guid.Empty,
                                             Quantity = 5,
                                             SortOrder = 2,
                                             Substance = "water",
                                             UnitName = "mL"
                                         }
                                 };
            return dto;
        }

        public static ProductDto GetProductDtoWithTwoSubstancesAndRoute()
        {
            var dto = GetProductDtoWithTwoSubstances();
            dto.Routes = new List<RouteDto> {new RouteDto {Id = Guid.Empty, Name = Route, Abbreviation = Route}};
            return dto;
        }

        public static IEnumerable<ProductDto> GetProductDtoListWithThreeItems()
        {
            var dto = GetProductDtoWithNoSubstances();
            var list = new List<ProductDto>();
            list.Add(dto);
            dto = dto.CloneDto();
            dto.Name = "paracetamol Paracetamol zetpil 1 stuk";
            dto.BrandName = "Paracetamol";
            dto.GenericName = "paracetamol";
            dto.PackageName = "zetpil";
            dto.Quantity = 500;
            dto.ShapeName = "zetpil";
            dto.UnitName = "stuk";
            list.Add(dto);
            dto = dto.CloneDto();
            dto.Name = "lactulose Duphalac stroop 200 milliliter";
            dto.BrandName = "Duphalac";
            dto.GenericName = "lactulose";
            dto.ShapeName = "stroop";
            dto.Quantity = 200;
            dto.UnitName = "milliliter";
            list.Add(dto);

            return list;
        }

        public static ProductDto GetProductDtoWithOneSubstanceAndRoutes()
        {
            var dto = GetProductDtoWithOneSubstance();
            AddOneRoute(dto);
            return dto;
        }

        private static void AddOneRoute(ProductDto dto)
        {
            dto.Routes = new List<RouteDto>
                             {
                                 new RouteDto {Abbreviation = "iv", Name = "intraveneus"}
                             };
        }

        public static ProductDto GetProductWithOneRoute()
        {
            var dto = GetProductDtoWithNoSubstances();
            AddOneRoute(dto);
            return dto;
        }

        public static Product CreateProductWithOneSubstAndOneRoute()
        {
            var dto = GetProductDtoWithNoSubstances();
            var shape = ShapeTestFixtures.CreateIvFluidShape();
            var package = PackageTestFixtures.CreatePackageAmpul();
            var unit = UnitTestFixtures.CreateUnitMililiter();
            var product = Product.Create(dto)
                .Shape(shape)
                .Package(package)
                .Quantity(unit, 5M)
                .Substance(1, SubstanceTestFixtures.CreateSubstanceWithoutGroup(), 200M,
                           UnitTestFixtures.CreateUnitMilligram())
                .Route(RouteTestFixtures.CreateRouteIv());

            return product;
        }
    }
}
