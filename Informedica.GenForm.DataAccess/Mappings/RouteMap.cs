using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class RouteMap : EntityMap<Route>
    {
        public RouteMap() : base(Route.NameLength)
        {
            Map(r => r.Abbreviation).Not.Nullable().Length(Route.AbbreviationLength).Unique();
            HasManyToMany(r => r.ShapeSet)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All()
                .Inverse();
            HasManyToMany(r => r.ProductSet)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All()
                .Inverse();
        }
    }
}
