using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class ProductFactory : EntityFactory<Product, ProductDto>
    {
        public ProductFactory(ProductDto dto) : base(dto) {}

        protected override Product Create()
        {
            var substDto = Dto.Substances.First();
            Dto.Substances = Dto.Substances.Skip(1).ToList();

            var routeDto = Dto.Routes.First();
            Dto.Routes = Dto.Routes.Skip(1).ToList();

            var product = Product.Create(GetProductDto())
                .Shape(GetShape())
                .Package(GetPackage())
                .Quantity(GetUnitValue().Unit, GetUnitValue().Value)
                .Substance(substDto.SortOrder, GetSubstance(substDto.Substance), substDto.Quantity, GetUnit(substDto))
                .Route(GetRoute(routeDto));

            if (!String.IsNullOrWhiteSpace(Dto.BrandName)) product.SetBrand(GetBrand());

            product = AddProductSubstances(product);
            product = AddProductRoutes(product);

            Add(product);
            return product;
        }

        private Product AddProductRoutes(Product product)
        {
            foreach (var dto in Dto.Routes)
            {
                product.AddRoute(GetRoute(dto));
            }
            return product;
        }

        private Route GetRoute(RouteDto dto)
        {
            return new RouteFactory(dto).Get();
        }

        private Product AddProductSubstances(Product product)
        {
            foreach (var dto in Dto.Substances)
            {
                product.AddSubstance(dto.SortOrder, GetSubstance(dto.Substance), dto.Quantity, GetUnit(dto));
            }
            return product;
        }

        private Unit GetUnit(ProductSubstanceDto dto)
        {
            var unitDto = GetUnitDto(dto);
            return new UnitFactory(unitDto).Get();
        }

        private UnitDto GetUnitDto()
        {
            var unitDto = new UnitDto
            {
                Abbreviation = Dto.UnitAbbreviation,
                AllowConversion = Dto.UnitGroupAllowConversion,
                Divisor = Dto.UnitDivisor,
                IsReference = Dto.UnitIsReference,
                Multiplier = Dto.UnitMultiplier,
                Name = Dto.UnitName,
                UnitGroupName = Dto.UnitGroupName
            };
            return unitDto;
            
        }

        private static UnitDto GetUnitDto(ProductSubstanceDto dto)
        {
            var unitDto = new UnitDto
                              {
                                  Abbreviation = dto.UnitAbbreviation,
                                  AllowConversion = dto.UnitGroupAllowConversion,
                                  Divisor = dto.UnitDivisor,
                                  IsReference = dto.UnitIsReference,
                                  Multiplier = dto.UnitMultiplier,
                                  Name = dto.UnitName,
                                  UnitGroupName = dto.UnitGroupName
                              };
            return unitDto;
        }

        private Substance GetSubstance(string substance)
        {
            return new SubstanceFactory(new SubstanceDto {Name = substance}).Get();
        }


        private UnitValue GetUnitValue()
        {
            return new UnitValue(Dto.Quantity, GetUnit());
        }

        private Unit GetUnit()
        {
            return new UnitFactory(GetUnitDto()).Get();
        }

        private UnitGroupDto GetUnitGroupDto()
        {
            return new UnitGroupDto
            {
                AllowConversion = Dto.UnitGroupAllowConversion,
                Name = Dto.UnitGroupName
            };
        }

        private Package GetPackage()
        {
            return new PackageFactory(GetPackageDto()).Get();
        }

        private PackageDto GetPackageDto()
        {
            return new PackageDto
                       {
                           Abbreviation = Dto.PackageAbbreviation, 
                           Name = Dto.PackageName
                       };
        }

        private Shape GetShape()
        {
            return new ShapeFactory(GetShapeDto()).Get();
        }

        private ShapeDto GetShapeDto()
        {
            return new ShapeDto
            {
                Name = Dto.ShapeName,
                Packages = new List<PackageDto> { GetPackageDto()},
                UnitGroups = new List<UnitGroupDto> { GetUnitGroupDto()}
            };
        }

        private Brand GetBrand()
        {
            return new BrandFactory(new BrandDto {Name = Dto.BrandName}).Get();
        }

        private ProductDto GetProductDto()
        {
            return new ProductDto
                       {
                           DisplayName = Dto.DisplayName,
                           GenericName = Dto.GenericName,
                           Name =  Dto.Name,
                           ProductCode = Dto.ProductCode
                       };
        }
    }
}