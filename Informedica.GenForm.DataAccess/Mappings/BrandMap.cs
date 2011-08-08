using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class BrandMap : ClassMap<Brand>
    {
        public BrandMap()
        {
            Id(b => b.Id).GeneratedBy.GuidComb();
            Map(b => b.Name).Not.Nullable().Length(255).Unique();
        }
    }
}