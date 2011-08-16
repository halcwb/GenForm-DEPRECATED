using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Shape: Entity<Guid, ShapeDto>, IShape
    {
        private HashSet<Package> _packages = new HashSet<Package>(new PackageComparer());
        private HashSet<Unit> _units = new HashSet<Unit>(new UnitComparer());
        private HashSet<Route> _routes = new HashSet<Route>(new RouteComparer());
        private readonly HashSet<Product> _products = new HashSet<Product>(new ProductComparer());

        #region Implementation of IShape

        protected Shape():base(new ShapeDto()) {}

        private Shape(ShapeDto dto) : base(dto.CloneDto())
        {
            AddPackages();
            AddUnits();
            AddRoutes();
        }

        private void AddRoutes()
        {
            foreach (var route in Dto.Routes)
            {
                AddRoute(Route.Create(route));   
            }
        }

        public virtual void AddRoute(Route route)
        {
            route.AddShape(this);
        }

        protected internal virtual void AddRoute(Route route, Action<Shape> addShapeToRoute)
        {
            if (_routes.Contains(route, _routes.Comparer)) throw new CannotAddItemException<Route>(route);
            _routes.Add(route);
            addShapeToRoute(this);
        }

        public virtual bool CanNotAddRoute(Route route)
        {
            if (route == null) return true;
            return _routes.Contains(route, _routes.Comparer);
        }

        private void AddPackages()
        {            
            foreach (var package in Dto.Packages)
            {
                AddPackage(Package.Create(package));
            }
        }

        private void AddUnits()
        {
            foreach (var unit in Dto.Units)
            {
                AddUnit(Unit.Create(unit));
            }
        }

        public virtual void AddUnit(Unit unit)
        {
            unit.AddShape(this);
        }

        protected internal virtual void AddUnit(Unit unit, Action<Shape> addShapeToUnit)
        {
            if (_units.Contains(unit, _units.Comparer)) throw new CannotAddItemException<Unit>(unit);
            _units.Add(unit);
            addShapeToUnit(this);
        }

        public virtual bool CanNotAddUnit(Unit unit)
        {
            if (unit == null) return true;
            return _units.Contains(unit, _units.Comparer);
        }

        public virtual void AddPackage(Package package)
        {
            package.AddShape(this);
        }

        protected internal virtual void AddPackage(Package package, Action<Shape> addShapeToPackage)
        {
            if (_packages.Contains(package, new PackageComparer())) throw new CannotAddItemException<Package>(package);
            _packages.Add(package);
            addShapeToPackage(this);
        }

        public virtual bool CanNotAddPackage(Package package)
        {
            if (package == null) return true;
            return _packages.Contains(package, _packages.Comparer);
        }

        public virtual IEnumerable<Package> Packages
        {
            get { return _packages; }
            protected set { _packages = new HashSet<Package>(value); }
        }

        public virtual IEnumerable<Unit> Units
        {
            get { return _units; }
            protected set { _units = new HashSet<Unit>(value); }
        }

        public virtual IEnumerable<Route> Routes
        {
            get { return _routes; }
            protected set { _routes = new HashSet<Route>(value); }
        }

        public virtual IEnumerable<Product> Products
        {
            get { return _products; }
        }

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public virtual void RemovePackage(Package package)
        {
            _packages.Remove(package);
        }

        public static Shape Create(ShapeDto dto)
        {
            return new Shape(dto);
        }

        internal protected virtual void AddProduct(Product product)
        {
            product.SetShape(this, AddProductToShape);
        }

        private void AddProductToShape(Product product)
        {
            _products.Add(product);
        }

        internal protected virtual void Remove(Product product)
        {
            _products.Remove(product);
        }
    }
}
