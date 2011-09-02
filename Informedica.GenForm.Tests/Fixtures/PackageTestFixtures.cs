using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class PackageTestFixtures
    {
        public static PackageDto GetAmpulDto()
        {
            return new PackageDto
                       {
                           Abbreviation = "ampul",
                           Name = "ampul"
                       };
        }

        public static PackageDto GetDtoWithOneShape()
        {
            var dto = GetAmpulDto();
            dto.Shapes = new List<ShapeDto> {ShapeTestFixtures.GetIvFluidDto()};
            return dto;
        }

        public static PackageDto GetDtoWithTwoShapes()
        {
            var dto = GetDtoWithOneShape();
            dto.Shapes.Add(new ShapeDto {Name = "injectievloeistof"});
            return dto;
        }

        public static Package CreatePackageAmpul()
        {
            return Package.Create(GetAmpulDto());
        }
    }
}