using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class SubstanceMap: ClassMap<Substance>
    {
        public SubstanceMap()
        {
            Id(x => x.Id).Not.Nullable().GeneratedBy.GuidComb();
            Map(x => x.Name).Not.Nullable().Length(255).Unique();
            References(x => x.SubstanceGroup).Cascade.All();
        }
    }
}
