using System;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Shape: Entity<Guid, ShapeDto>, IShape
    {
        private ISet<Route> _routes = new HashedSet<Route>();
        private ISet<UnitGroup> _unitGroups = new HashedSet<UnitGroup>();
        private ISet<Package> _packages = new HashedSet<Package>();
        private ISet<Product> _products = new HashedSet<Product>();

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
            if (_routes.Contains(route)) return;

            _routes.Add(route);
            route.AddShape(this);
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
            if (_unitGroups.Contains(group)) return;

            _unitGroups.Add(group);
            group.AddShape(this);
        }

        public virtual void AddPackage(Package package)
        {
            if (_packages.Contains(package)) return;
        }

        public virtual ISet<Package> Packages
        {
            get { return _packages; }
            protected set { _packages = value; }
        }

        public virtual ISet<UnitGroup> UnitGroups
        {
            get { return _unitGroups; }
            protected set { _unitGroups = value; }
        }

        public virtual ISet<Route> Routes
        {
            get { return _routes; }
            protected set { _routes = value; }
        }

        public virtual ISet<Product> Products
        {
            get { return _products; }
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public virtual void RemovePackage(Package package)
        {
            if (!_packages.Contains(package)) return;
            
            _packages.Remove(package);
            package.RemoveShape(this);
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
            var list = new HashedSet<Route>(Routes);
            foreach (var route in list)
            {
                RemoveRoute(route);
            }
        }

        private void RemoveAllUnitGroups()
        {
            var list = new HashedSet<UnitGroup>(UnitGroups);
            foreach (var unitGroup in list)
            {
                RemoveUnitGroup(unitGroup);
            }
        }

        internal protected virtual void RemoveAllPackages()
        {
            var list = new HashedSet<Package>(Packages);
            foreach (var package in list)
            {
                RemovePackage(package);
            }
        }

        public virtual void RemoveUnitGroup(UnitGroup unitGroup)
        {
            if (!_unitGroups.Contains(unitGroup)) return;
            
            _unitGroups.Remove(unitGroup);
            unitGroup.RemoveShape(this);
        }

        public void RemoveRoute(Route route)
        {
            if (!_routes.Contains(route)) return;
            
            _routes.Remove(route);
            route.RemoveShape(this);
        }

        internal protected virtual void RemoveProduct(Product product)
        {
            if (_products.Contains(product)) _products.Remove(product);
        }

        public void AddProduct(Product product)
        {
            if (_products.Contains(product)) return;

            _products.Add(product);
            product.SetShape(this);
        }
    }
}
