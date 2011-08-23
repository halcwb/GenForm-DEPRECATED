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
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All()
                .Inverse();
            HasManyToMany(r => r.Products)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All()
                .Inverse();
        }
    }
}
