using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.TestFixtures.Fixtures
{
    public static class RouteTestFixtures
    {
        public static RouteDto GetRouteIvDto()
        {
            return new RouteDto
                       {
                           Abbreviation = "iv",
                           Name = "intraveneus"
                       };
        }

        public static RouteDto GetRouteWithShape()
        {
            var dto = GetRouteIvDto();
            dto.Shapes = new List<ShapeDto>
                             {
                                 ShapeTestFixtures.GetIvFluidDto()
                             };
            return dto;
        }

        public static Route CreateRouteIv()
        {
            return Route.Create(GetRouteIvDto());
        }
    }
}