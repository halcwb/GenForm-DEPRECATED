using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public class PackageMap : EntityMap<Package>
    {
        public PackageMap()
        {
            Map(p => p.Abbreviation).Not.Nullable().Length(30).Unique();
            HasManyToMany(p => p.ShapeSet)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Inverse()
                .Cascade.All();
        }
    }
}
