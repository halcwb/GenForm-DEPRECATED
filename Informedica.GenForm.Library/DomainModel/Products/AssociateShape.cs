using System.Collections.Generic;
using System.Linq;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public static class AssociateShape
    {
        public static void WithRoute<T>(Shape shape, Route route, ISet<T> set)
        {
            if (typeof(T) == typeof(Shape)) WithRoute(shape, route, set as ISet<Shape>);
            if (typeof(T) == typeof(Route)) WithRoute(shape, route, set as ISet<Route>);
        }

        private static void WithRoute(Shape shape, Route route, ISet<Shape> shapes)
        {
            shapes.Add(shape);
            shape.AddRoute(route);
        }

        private static void WithRoute(Shape shape, Route route, ISet<Route> routes)
        {
            routes.Add(route);
            route.AddShape(shape);
        }

        public static void WithPackage<T>(Shape shape, Package package, ISet<T> set)
        {
            if (typeof(T) == typeof(Shape)) WithPackage(shape, package, set as ISet<Shape>);
            if (typeof(T) == typeof(Package)) WithPackage(shape, package, set as ISet<Package>);
        }

        private static void WithPackage(Shape shape, Package package, ISet<Shape> shapes)
        {
            shapes.Add(shape);
            shape.AddPackage(package);
        }

        private static void WithPackage(Shape shape, Package package, ISet<Package> packages)
        {
            packages.Add(package);
            package.AddShape(shape);
        }

        public static void WithUnit<T>(Shape shape, Unit unit, ISet<T> set)
        {
            if (typeof(T) == typeof(Shape)) WithUnit(shape, unit, set as ISet<Shape>);
            if (typeof(T) == typeof(Unit)) WithUnit(shape, unit, set as ISet<Unit>);
        }

        private static void WithUnit(Shape shape, Unit unit, ISet<Shape> shapes)
        {
            shapes.Add(shape);
            shape.AddUnit(unit);
        }

        private static void WithUnit(Shape shape, Unit unit, ISet<Unit> units)
        {
            units.Add(unit);
            unit.AddShape(shape);
        }

        public static bool CanNotAddShape(Shape shape, HashSet<Shape> shapes)
        {
            if (shape == null) return true;
            return shapes.Contains(shape, shapes.Comparer);
        }
    }
}