using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public class UnitMap: EntityMap<Unit>
    {
        public UnitMap()
        {
            Map(x => x.Abbreviation).Not.Nullable().Length(30);
            Map(x => x.Multiplier);
            Map(x => x.IsReference);
            References(x => x.UnitGroup)
                .Cascade
                .SaveUpdate()
                .Not.Nullable();
        }
    }
}
