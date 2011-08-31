using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class BrandMap : EntityMap<Brand>
    {
        public BrandMap()
        {
            HasMany(x => x.Products)
                .AsSet()
                .Inverse();
        }
    }
}