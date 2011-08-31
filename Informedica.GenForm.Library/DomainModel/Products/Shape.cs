using System;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Shape: Entity<Shape>, IShape
    {
        private ISet<Route> _routes = new HashedSet<Route>();
        private ISet<UnitGroup> _unitGroups = new HashedSet<UnitGroup>();
        private ISet<Package> _packages = new HashedSet<Package>();
        private readonly ISet<Product> _products = new HashedSet<Product>();

        private ShapeDto _dto;

        static Shape()
        {
            RegisterValidationRules();
        }

        protected Shape():base(new ShapeComparer())
        {
            _dto = new ShapeDto();
        }

        private Shape(ShapeDto dto) : base(new ShapeComparer())
        {
            ValidateDto(dto);

            AddPackages();
            AddUnitGroups();
            AddRoutes();
        }

        private void AddRoutes()
        {
            foreach (var route in _dto.Routes)
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
            foreach (var package in _dto.Packages)
            {
                AddPackage(Package.Create(package));
            }
        }

        private void AddUnitGroups()
        {
            foreach (var dto in _dto.UnitGroups)
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

            _packages.Add(package);
            package.AddShape(this);
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

        public virtual void RemoveRoute(Route route)
        {
            if (!_routes.Contains(route)) return;
            
            _routes.Remove(route);
            route.RemoveShape(this);
        }

        internal protected virtual void RemoveProduct(Product product)
        {
            if (_products.Contains(product)) _products.Remove(product);
        }

        public virtual void AddProduct(Product product)
        {
            if (_products.Contains(product)) return;

            _products.Add(product);
            product.SetShape(this);
        }

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<ShapeDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<ShapeDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();            
        }
    }
}
