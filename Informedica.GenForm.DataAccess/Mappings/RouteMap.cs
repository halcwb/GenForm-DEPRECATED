using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class RouteMap : EntityMap<Route, Guid, RouteDto>
    {
        public RouteMap()
        {
            Map(r => r.Abbreviation).Not.Nullable().Length(30).Unique();
            HasManyToMany(r => r.Shapes)
                .AsSet()
                .Cascade.All()
                .Inverse();
            HasManyToMany(r => r.Products)
                .AsSet()
                .Cascade.All()
                .Inverse();
        }
    }
}
