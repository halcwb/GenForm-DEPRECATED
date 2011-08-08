using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class SubstanceGroupMap: ClassMap<SubstanceGroup>
    {
        public SubstanceGroupMap()
        {
            Id(x => x.Id).Not.Nullable().GeneratedBy.GuidComb();
            Map(x => x.Name).Not.Nullable().Length(255);
            HasMany(x => x.Substances).Cascade.SaveUpdate();
        }
    }
}
