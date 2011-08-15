using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class SubstanceGroupMap: EntityMap<SubstanceGroup, Guid, SubstanceGroupDto>
    {
        public SubstanceGroupMap()
        {
            HasMany(x => x.Substances).Cascade.SaveUpdate();
        }
    }
}
