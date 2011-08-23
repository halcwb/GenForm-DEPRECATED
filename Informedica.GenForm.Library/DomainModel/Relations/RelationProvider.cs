using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Relations
{
    internal static class RelationProvider
    {
        internal static OneToManyRelation<Package, Product> PackageProduct
        {
            get { return RelationManager.OneToMany<Package, Product>(); }
        }

        internal static OneToManyRelation<Shape, Product> ShapeProduct
        {
            get { return RelationManager.OneToMany<Shape, Product>(); }
        }

        internal static ManyToManyRelation<Shape, Route> ShapeRoute
        {
            get { return RelationManager.ManyToMany<Shape, Route>(); }
        }

        internal static ManyToManyRelation<Shape, UnitGroup> ShapeUnitGroup
        {
            get { return RelationManager.ManyToMany<Shape, UnitGroup>(); }
        }

        internal static ManyToManyRelation<Shape, Package> ShapePackage
        {
            get { return RelationManager.ManyToMany<Shape, Package>(); }
        }

        internal static ManyToManyRelation<Route, Product> RouteProduct
        {
            get { return RelationManager.ManyToMany<Route, Product>(); }
        }

        internal static OneToManyRelation<Brand, Product> BrandProduct
        {
            get { return RelationManager.OneToMany<Brand, Product>(); }
        }

        internal static OneToManyRelation<UnitGroup, Unit> UnitGroupUnit
        {
            get { return RelationManager.OneToMany<UnitGroup, Unit>(); }
        }

        public static OneToManyRelation<SubstanceGroup, Substance> SubstanceGroupSubstance
        {
            get { return RelationManager.OneToMany<SubstanceGroup, Substance>(); }
        }
    }
}
