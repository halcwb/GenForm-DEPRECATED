using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class RouteFactory : EntityFactory<Route, RouteDto>
    {
        public RouteFactory(RouteDto dto) : base(dto) {}

        protected override Route Create()
        {
            var route = Route.Create(Dto);
            Add(route);
            return route;
        }
    }
}