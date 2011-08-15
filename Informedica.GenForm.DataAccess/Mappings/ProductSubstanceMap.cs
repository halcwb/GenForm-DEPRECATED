using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ProductSubstanceMap : ClassMap<ProductSubstance>
    {
        private volatile string ProductSubstanceCombi = "ProductSubstanceCombi";

        public ProductSubstanceMap()
        {
            Id(x => x.Id).GeneratedBy.HiLo("1");
            Map(x => x.SortOrder).Not.Nullable();
            References(x => x.Substance).Not.Nullable()
                .UniqueKey(ProductSubstanceCombi)
                .Cascade.SaveUpdate();
            References(x => x.Product).Not.Nullable()
                .UniqueKey(ProductSubstanceCombi);
            Component(x => x.Quantity, UnitValueMap.GetMap());

        }
    }
}