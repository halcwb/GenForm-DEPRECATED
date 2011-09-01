using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class UnitGroupMap: EntityMap<UnitGroup>
    {
        public UnitGroupMap()
        {
            Map(x => x.AllowsConversion);
            HasMany(x => x.UnitSet)
                .Cascade.All().Inverse();
            HasManyToMany(x => x.ShapeSet)
                .Fetch.Join()
                .AsSet()
                .Inverse()
                .Cascade.All();
        }
    }
}
