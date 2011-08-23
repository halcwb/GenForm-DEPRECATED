using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class RouteTestFixtures
    {
        public static RouteDto GetValidDto()
        {
            return new RouteDto
                       {
                           Abbreviation = "iv",
                           Name = "intraveneus"
                       };
        }

        public static RouteDto GetRouteWithShape()
        {
            var dto = GetValidDto();
            dto.Shapes = new List<ShapeDto>
                             {
                                 ShapeTestFixtures.GetValidDto()
                             };
            return dto;
        }
    }
}