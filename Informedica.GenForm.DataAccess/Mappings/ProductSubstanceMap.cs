using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ProductSubstanceMap : ClassMap<ProductSubstance>
    {
        private const string ProductSubstanceCombi = "ProductSubstanceCombi";

        public ProductSubstanceMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.SortOrder).Not.Nullable();
            References<Substance>(x => x.Substance).Not.Nullable()
                .Cascade.SaveUpdate()
                .UniqueKey(ProductSubstanceCombi);
            References(x => x.Product).Not.Nullable()
                .Cascade.SaveUpdate()
                .UniqueKey(ProductSubstanceCombi);
            Component(x => x.Quantity, UnitValueMap.GetMap());
        }
    }
}