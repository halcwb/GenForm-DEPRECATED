using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class SubstanceGroupMap: EntityMap<SubstanceGroup>
    {
        public SubstanceGroupMap()
        {
            HasMany(x => x.SubstanceSet)
                .AsSet()
                .Cascade.All()
                .Inverse();
        }
    }
}
