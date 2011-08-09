using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ShapeMap: ClassMap<Shape>
    {
        public ShapeMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.Name).Not.Nullable().Length(255);
            HasManyToMany(s => s.Packages).Cascade.SaveUpdate();
            HasManyToMany(s => s.Units).Cascade.SaveUpdate();
            HasManyToMany(s => s.Routes)
                .Cascade.AllDeleteOrphan()
                .Cascade.SaveUpdate();
        }
    }
}
