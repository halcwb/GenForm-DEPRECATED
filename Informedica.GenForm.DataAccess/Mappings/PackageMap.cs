using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public class PackageMap : EntityMap<Package, Guid, PackageDto>
    {
        public PackageMap()
        {
            Map(p => p.Abbreviation).Not.Nullable().Length(30).Unique();
            HasManyToMany(p => p.Shapes)
                .Inverse()
                .Cascade.All();
        }
    }
}
