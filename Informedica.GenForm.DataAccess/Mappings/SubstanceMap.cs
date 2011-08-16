using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class SubstanceMap: EntityMap<Substance, Guid, SubstanceDto>
    {
        public SubstanceMap()
        {
            References(x => x.SubstanceGroup).Cascade.All();
        }
    }
}
