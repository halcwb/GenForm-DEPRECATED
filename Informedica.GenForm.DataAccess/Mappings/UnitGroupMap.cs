using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class UnitGroupMap: EntityMap<UnitGroup, Guid, UnitGroupDto>
    {
        public UnitGroupMap()
        {
            Map(x => x.AllowsConversion);
            HasMany(x => x.Units)
                .Cascade.All().Inverse();
        }
    }
}
