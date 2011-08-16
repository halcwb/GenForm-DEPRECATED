using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using NHibernate.Collection.Generic;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class ShapeTestFixtures
    {
        public static ShapeDto GetValidDto()
        {
            return new ShapeDto
                       {
                           Name = "infusievloeistof"
                       };
        }

        public static ShapeDto GetValidDtoWithPackages()
        {
            var dto = GetValidDto();
            dto.Packages = new List<PackageDto>
                {
                    new PackageDto{ Abbreviation = "ampul", Name = "ampul"},
                    new PackageDto{ Abbreviation = "zak", Name = "zak"}
                };
            return dto;
        }

        public static ShapeDto GetValidDtoWithRoutes()
        {
            var dto = GetValidDtoWithPackages();
            dto.Routes = new List<RouteDto>
                             {
                                 RouteTestFixtures.GetValidDto(),
                                 new RouteDto{ Abbreviation = "or", Name = "oraal"}
                             };
            return dto;
        }

        public static ShapeDto GetValidDtoWithUnits()
        {
            var dto = GetValidDtoWithRoutes();
            dto.Units = new List<UnitDto>
                            {
                                UnitTestFixtures.GetTestUnitMilligram(),
                                new UnitDto{ Abbreviation = "mL", AllowConversion = true, Divisor = 1000, IsReference = false, Name = "milliliter", Multiplier = 0.001M, UnitGroupName = "volume"}
                            };
            return dto;
        }
    }
}