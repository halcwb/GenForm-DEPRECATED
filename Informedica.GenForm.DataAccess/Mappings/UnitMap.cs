using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public class UnitMap: EntityMap<Unit, Guid, UnitDto>
    {
        public UnitMap()
        {
            Map(x => x.Abbreviation);
            Map(x => x.Multiplier);
            Map(x => x.IsReference);
            References(x => x.UnitGroup)
                .Cascade
                .SaveUpdate()
                .Not.Nullable(); 
        }
    }
}
