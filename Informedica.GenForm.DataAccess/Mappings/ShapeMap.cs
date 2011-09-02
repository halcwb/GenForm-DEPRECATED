using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class ShapeMap: EntityMap<Shape>
    {
        public ShapeMap()
        {
            HasManyToMany(s => s.PackageSet).Table("ShapeToPackage")
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.SaveUpdate();
            HasManyToMany(s => s.UnitGroupSet).Table("ShapeToUnitGroup")
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.SaveUpdate();
            HasManyToMany(s => s.RouteSet).Table("ShapeToRoute")
                // Fetch.Join will raise laizy collection load error
                .Fetch.Select()
                .AsSet()
                .Cascade.SaveUpdate();
        }
    }
}
