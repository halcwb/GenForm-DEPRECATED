using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ShapeMap: EntityMap<Shape, Guid, ShapeDto>
    {
        public ShapeMap()
        {
            HasManyToMany(s => s.Packages).AsSet()
                .Cascade.All();
            HasManyToMany(s => s.Units).AsSet()
                .Cascade.All();
            HasManyToMany(s => s.Routes).AsSet()
                .Cascade.All();
        }
    }
}
