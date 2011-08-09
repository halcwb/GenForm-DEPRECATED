using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public class PackageMap : ClassMap<Package>
    {
        public PackageMap()
        {
            Id(p => p.Id).GeneratedBy.GuidComb();
            Map(p => p.Name).Not.Nullable().Length(255).Unique();
            Map(p => p.Abbreviation).Not.Nullable().Length(30).Unique();
        }
    }
}
