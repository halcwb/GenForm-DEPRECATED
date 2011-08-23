using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class PackageTestFixtures
    {
        public static PackageDto GetValidDto()
        {
            return new PackageDto
                       {
                           Abbreviation = "ampul",
                           Name = "ampul"
                       };
        }

        public static PackageDto GetDtoWithOneShape()
        {
            var dto = GetValidDto();
            dto.Shapes = new List<ShapeDto> {ShapeTestFixtures.GetValidDto()};
            return dto;
        }

        public static PackageDto GetDtoWithTwoShapes()
        {
            var dto = GetDtoWithOneShape();
            dto.Shapes.Add(new ShapeDto {Name = "injectievloeistof"});
            return dto;
        }
    }
}