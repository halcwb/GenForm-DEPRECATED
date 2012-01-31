using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.TestFixtures.Fixtures
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
                                             Id = Guid.Empty.ToString(),
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
            dto.Routes = new List<RouteDto> {new RouteDto {Id = Guid.Empty.ToString(), Name = Route, Abbreviation = Route}};
            return dto;
        }

        public static IEnumerable<ProductDto> GetProductDtoListWithThreeItems()
        {
            var dto = GetProductDtoWithNoSubstances();
            var list = new List<ProductDto> {dto};
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

        public static ProductDto GetParacetamolDto()
        {
            var subst = new ProductSubstanceDto
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "paracetamol",
                                Quantity = 500,
                                SortOrder = 1,
                                Substance = "paracetamol",
                                UnitAbbreviation = "mg",
                                UnitDivisor = 1000,
                                UnitGroupAllowConversion = true,
                                UnitGroupName = "massa",
                                UnitIsReference = false,
                                UnitMultiplier = 1/1000,
                                UnitName = "milligram"
                            };

            var route = new RouteDto
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "oraal",
                                Abbreviation = "or"
                            };

            var dto = new ProductDto
                          {
                              BrandName = "",
                              GenericName = "paracetamol",
                              DisplayName = "paracetamol 500 mg tablet",
                              Id = Guid.NewGuid().ToString(),
                              Name = DisplayName,
                              PackageAbbreviation = "tabl",
                              PackageName = "tablet",
                              ProductCode = "2",
                              Quantity = 1M,
                              Routes = new List<RouteDto> { route },
                              ShapeName = "tablet",
                              Substances = new List<ProductSubstanceDto> {subst},
                              UnitAbbreviation = "stuk",
                              UnitDivisor = 1,
                              UnitGroupAllowConversion = false,
                              UnitGroupName = "verpakking",
                              UnitIsReference = false,
                              UnitMultiplier = 1,
                              UnitName = "stuk"
                          };
            return dto;
        }

        public static ProductDto GetDopamineDto()
        {
            var subst = new ProductSubstanceDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = "dopamine",
                Quantity = 200,
                SortOrder = 1,
                Substance = "dopamine",
                UnitAbbreviation = "mg",
                UnitDivisor = 1000,
                UnitGroupAllowConversion = true,
                UnitGroupName = "massa",
                UnitIsReference = false,
                UnitMultiplier = 1 / 1000,
                UnitName = "milligram"
            };

            var dto = new ProductDto
            {
                BrandName = "Dynatra",
                GenericName = "dopamine",
                DisplayName = "dopamine 200 mg (Dynatra) 5 mL infusievloeistof per ampul",
                Id = Guid.NewGuid().ToString(),
                Name = DisplayName,
                PackageAbbreviation = "amp",
                PackageName = "ampul",
                ProductCode = "1",
                Quantity = 5M,
                Routes = new List<RouteDto> { RouteTestFixtures.GetRouteIvDto() },
                ShapeName = "infusievloeistof",
                Substances = new List<ProductSubstanceDto> { subst },
                UnitAbbreviation = "mL",
                UnitDivisor = 1000,
                UnitGroupAllowConversion = true,
                UnitGroupName = "volume",
                UnitIsReference = false,
                UnitMultiplier = 1 / 1000,
                UnitName = "milliliter"
            };
            return dto;
        }
    
    }
}
