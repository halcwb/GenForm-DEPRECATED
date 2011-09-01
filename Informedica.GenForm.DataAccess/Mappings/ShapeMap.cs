using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ShapeMap: EntityMap<Shape>
    {
        public ShapeMap()
        {
            HasManyToMany(s => s.PackageSet)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All();
            HasManyToMany(s => s.UnitGroupSet)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All();
            HasManyToMany(s => s.RouteSet)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All();
        }
    }
}
