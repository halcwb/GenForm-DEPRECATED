using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Shape: Entity<Guid, ShapeDto>, IShape
    {
        private HashSet<Package> _packages = new HashSet<Package>(new PackageComparer());
        private HashSet<Unit> _units = new HashSet<Unit>(new UnitComparer());
        private HashSet<Route> _routes = new HashSet<Route>(new RouteComparer());

        #region Implementation of IShape

        protected Shape():base(new ShapeDto()) {}

        public Shape(ShapeDto dto) : base(dto.CloneDto())
        {
            AddPackages();
            AddUnits();
            AddRoutes();
        }

        private void AddRoutes()
        {
            foreach (var route in Dto.Routes)
            {
                AddRoute(new Route(route));   
            }
        }

        public virtual void AddRoute(Route route)
        {
            if (CanNotAddRoute(route)) return;
            AssociateShape.WithRoute(this, route, _routes);
        }

        private bool CanNotAddRoute(Route route)
        {
            if (route == null) return true;
            return _routes.Contains(route, _routes.Comparer);
        }

        private void AddPackages()
        {            
            foreach (var package in Dto.Packages)
            {
                AddPackage(new Package(package));
            }
        }

        private void AddUnits()
        {
            foreach (var unit in Dto.Units)
            {
                AddUnit(new Unit(unit));
            }
        }

        public virtual void AddUnit(Unit unit)
        {
            if (CanNotAddUnit(unit)) return;
            AssociateShape.WithUnit(this, unit, _units);
        }

        public virtual bool CanNotAddUnit(Unit unit)
        {
            if (unit == null) return true;
            return _units.Contains(unit, _units.Comparer);
        }

        public virtual void AddPackage(Package package)
        {
            if (CanNotAddPackage(package)) return;
            AssociateShape.WithPackage(this, package, _packages);
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

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }
    }
}
