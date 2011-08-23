using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Relations;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Shape: Entity<Guid, ShapeDto>, IShape, IRelationPart
    {
        private HashSet<Package> _packages = new HashSet<Package>(new PackageComparer());
        private HashSet<UnitGroup> _unitGroups = new HashSet<UnitGroup>(new UnitGroupComparer());
        private HashSet<Route> _routes = new HashSet<Route>(new RouteComparer());

        #region Implementation of IShape

        protected Shape():base(new ShapeDto()) {}

        private Shape(ShapeDto dto) : base(dto.CloneDto())
        {
            AddPackages();
            AddUnitGroups();
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
            RelationProvider.ShapeRoute.Add(this, route);
            //route.AddShape(this);
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

        private void AddUnitGroups()
        {
            foreach (var dto in Dto.UnitGroups)
            {
                AddUnitGroup(UnitGroup.Create(dto));
            }
        }

        public virtual void AddUnitGroup(UnitGroup group)
        {
            RelationProvider.ShapeUnitGroup.Add(this, group);
        }

        public virtual bool CanNotAddUnitGroup(UnitGroup unitGroup)
        {
            if (unitGroup == null) return true;
            return _unitGroups.Contains(unitGroup, _unitGroups.Comparer);
        }

        public virtual void AddPackage(Package package)
        {
            RelationProvider.ShapePackage.Add(this, package);
        }

        public virtual bool CanNotAddPackage(Package package)
        {
            if (package == null) return true;
            return _packages.Contains(package, _packages.Comparer);
        }

        public virtual IEnumerable<Package> Packages
        {
            get { return RelationProvider.ShapePackage.GetManyPartRight(this); }
            protected set { RelationProvider.ShapePackage.Add(this, value); }
        }

        public virtual IEnumerable<UnitGroup> UnitGroups
        {
            get { return RelationProvider.ShapeUnitGroup.GetManyPartRight(this); }
            protected set { RelationProvider.ShapeUnitGroup.Add(this, value); }
        }

        public virtual IEnumerable<Route> Routes
        {
            get { return RelationProvider.ShapeRoute.GetManyPartRight(this); }
            protected set { RelationProvider.ShapeRoute.Add(this, value); }
        }

        public virtual IEnumerable<Product> Products
        {
            get { return RelationProvider.ShapeProduct.GetManyPart(this); }
        }

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public virtual void RemovePackage(Package package)
        {
            RelationProvider.ShapePackage.Remove(this, package);
        }

        public static Shape Create(ShapeDto dto)
        {
            return new Shape(dto);
        }

        internal protected virtual void RemoveAllAssociations()
        {
            RemoveAllPackages();
            RemoveAllUnitGroups();
            RemoveAllRoutes();
        }

        private void RemoveAllRoutes()
        {
            RelationProvider.ShapeRoute.Clear(this);
        }

        private void RemoveAllUnitGroups()
        {
            RelationProvider.ShapeUnitGroup.Clear(this);
        }

        internal protected virtual void RemoveAllPackages()
        {
            RelationProvider.ShapePackage.Clear(this);
        }

        public virtual void RemoveUnitGroup(UnitGroup unitGroup)
        {
            RelationProvider.ShapeUnitGroup.Remove(this, unitGroup);
        }
    }
}
