using System;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ShapeMap: EntityMap<Shape, Guid, ShapeDto>
    {
        public ShapeMap()
        {
            HasManyToMany(s => s.Packages)
                .Cascade.All();
            HasManyToMany(s => s.Units)
                .Cascade.AllDeleteOrphan()
                .Cascade.SaveUpdate();
            HasManyToMany(s => s.Routes)
                .Cascade.AllDeleteOrphan()
                .Cascade.SaveUpdate();
        }
    }
}
