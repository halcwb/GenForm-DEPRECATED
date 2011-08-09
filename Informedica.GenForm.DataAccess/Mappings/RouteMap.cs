using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class RouteMap : ClassMap<Route>
    {
        public RouteMap()
        {
            Id(r => r.Id).GeneratedBy.GuidComb();
            Map(r => r.Name).Not.Nullable().Length(255).Unique();
            Map(r => r.Abbreviation).Not.Nullable().Length(30).Unique();
            HasMany(r => r.Shapes)
                .Cascade.AllDeleteOrphan()
                .Cascade.SaveUpdate();
        }
    }
}
