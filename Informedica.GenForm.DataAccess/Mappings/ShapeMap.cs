using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ShapeMap: EntityMap<Shape>
    {
        public ShapeMap()
        {
            HasManyToMany(s => s.Packages)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All();
            HasManyToMany(s => s.UnitGroups)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All();
            HasManyToMany(s => s.Routes)
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.All();
        }
    }
}
