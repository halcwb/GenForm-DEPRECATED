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

        public static ShapeDto GetValidDtoWithUnitGroups()
        {
            var dto = GetValidDtoWithRoutes();
            dto.UnitGroups = new List<UnitGroupDto>
                            {
                                UnitGroupTestFixtures.GetDtoVolume(),
                                UnitGroupTestFixtures.GetDtoMass()
                            };
            return dto;
        }
    }
}