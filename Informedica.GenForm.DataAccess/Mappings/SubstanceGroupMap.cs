using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class SubstanceGroupMap: EntityMap<SubstanceGroup, Guid, SubstanceGroupDto>
    {
        public SubstanceGroupMap()
        {
            HasMany(x => x.Substances)
                .AsSet()
                .Inverse();
        }
    }
}
