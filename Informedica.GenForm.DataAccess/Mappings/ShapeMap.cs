using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ShapeMap: ClassMap<Shape>
    {
        public ShapeMap()
        {
            Id(s => s.Id);
            Map(s => s.Name);
        }
    }
}
