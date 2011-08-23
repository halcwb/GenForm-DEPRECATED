using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public class PackageMap : EntityMap<Package, Guid, PackageDto>
    {
        public PackageMap()
        {
            Map(p => p.Abbreviation).Not.Nullable().Length(30).Unique();
            HasManyToMany(p => p.Shapes)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Inverse()
                .Cascade.All();
        }
    }
}
