using Informedica.GenForm.DomainModel.Interfaces;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Services.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class RouteFactory : EntityFactory<Route, RouteDto>
    {
        public RouteFactory(RouteDto dto) : base(dto) {}

        protected override Route Create()
        {
            var route = Route.Create(Dto);

            foreach (var shape in Dto.Shapes)
            {
                route.AddShape(GetShape(shape));
            }

            Add(route);
            return route;
        }

        private IShape GetShape(ShapeDto shape)
        {
            return ShapeServices.WithDto(shape).Get();
        }
    }
}