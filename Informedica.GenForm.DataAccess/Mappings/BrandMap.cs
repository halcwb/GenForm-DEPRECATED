using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class BrandMap : EntityMap<Brand, Guid, BrandDto>
    {
        public BrandMap()
        {
            HasMany(x => x.Products)
                .AsSet()
                .Inverse();
        }
    }
}