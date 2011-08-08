using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public class UnitMap: ClassMap<Unit>
    {
        public UnitMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Abbreviation);
            Map(x => x.Multiplier);
            Map(x => x.IsReference);
            References<UnitGroup>(x => x.UnitGroup)
                .Cascade
                .SaveUpdate()
                .Not.Nullable(); 
        }
    }
}
