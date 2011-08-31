using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class UnitGroupMap: EntityMap<UnitGroup>
    {
        public UnitGroupMap()
        {
            Map(x => x.AllowsConversion);
            HasMany(x => x.Units)
                .Cascade.All().Inverse();
            HasManyToMany(x => x.Shapes)
                .Fetch.Join()
                .AsSet()
                .Inverse()
                .Cascade.All();
        }
    }
}
