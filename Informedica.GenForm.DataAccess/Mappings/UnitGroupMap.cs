using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public class UnitGroupMap: ClassMap<UnitGroup>
    {
        public UnitGroupMap()
        {
            Id(x => x.Id);
            Map(x => x.UnitGroupName);
            Map(x => x.AllowsConversion);
            HasMany(x => x.Units);
        }
    }
}
