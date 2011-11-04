using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Shape : Entity<Shape>, IShape
    {
        #region Private

        private UnitGroupSet _unitGroups;
        private PackageSet _packages;
        private RouteSet<Shape> _routes;
        private ProductSet<Shape> _products;

        #endregion

        #region Construction

        static Shape()
        {
            RegisterValidationRules();
        }

        protected Shape()
        {
            InitCollections();
        }


        private void InitCollections()
        {
            _unitGroups = new UnitGroupSet(this);
            _packages = new PackageSet(this);
            _routes = new RouteSet<Shape>(this);
            _products = new ProductSet<Shape>(this);
        }

        #endregion

        #region Business

        public virtual IEnumerable<IUnitGroup> UnitGroups
        {
            get { return _unitGroups; }
        }

        public virtual Iesi.Collections.Generic.ISet<UnitGroup> UnitGroupSet
        {
            get { return _unitGroups.GetEntitySet(); }
            protected set { _unitGroups = new UnitGroupSet(value, this); }
        }

        public virtual void AddUnitGroup(IUnitGroup unitGroup)
        {
            if (unitGroup == null) return;
            
            _unitGroups.Add((UnitGroup)unitGroup);
        }

        public virtual void RemoveUnitGroup(IUnitGroup unitGroup)
        {
            if (unitGroup == null) return;

            _unitGroups.Remove((UnitGroup)unitGroup);
        }

        public virtual IEnumerable<IPackage> Packages
        {
            get { return _packages; }
        }

        public virtual Iesi.Collections.Generic.ISet<Package> PackageSet
        {
            get { return _packages.GetEntitySet(); }
            protected set { _packages = new PackageSet(value, this); }
        }

        public virtual void AddPackage(IPackage package)
        {
            _packages.Add((Package)package);
        }

        public virtual void RemovePackage(IPackage package)
        {
            _packages.Remove((Package)package);
        }

        public virtual IEnumerable<IRoute> Routes
        {
           get { return _routes; }
        }
        
        public virtual Iesi.Collections.Generic.ISet<Route> RouteSet
        {
            get { return _routes.GetEntitySet(); }
            protected set { _routes = new RouteSet<Shape>(value, this); }
        }        
        
        public virtual void AddRoute(IRoute route)
        {
            _routes.Add((Route)route, ((Route)route).AddShape);
        }

        public virtual void RemoveRoute(IRoute route)
        {
            _routes.Remove((Route)route, ((Route)route).RemoveShape);
        }
        
        public virtual IEnumerable<IProduct> Products
        {
            get { return _products; }
        }

        public virtual Iesi.Collections.Generic.ISet<Product> ProductSet
        {
            get { return _products.GetEntitySet(); }
            protected set { _products = new ProductSet<Shape>(value, this);}
        }

        internal protected virtual void RemoveProduct(IProduct product)
        {
            if (ProductSet.Contains((Product)product)) ProductSet.Remove((Product)product);
        }

        public virtual void AddProduct(IProduct product)
        {
            _products.Add((Product) product, ((Product) product).SetShape);
        }

        internal protected virtual void RemoveAllAssociations()
        {
            RemoveAllPackages();
            RemoveAllUnitGroups();
            RemoveAllRoutes();
        }

        private void RemoveAllRoutes()
        {
            var list = new HashedSet<Route>(RouteSet);
            foreach (var route in list)
            {
                RemoveRoute(route);
            }
        }

        private void RemoveAllUnitGroups()
        {
            var list = new HashedSet<UnitGroup>(UnitGroupSet);
            foreach (var unitGroup in list)
            {
                RemoveUnitGroup(unitGroup);
            }
        }

        internal protected virtual void RemoveAllPackages()
        {
            var list = new HashedSet<Package>(PackageSet);
            foreach (var package in list)
            {
                RemovePackage(package);
            }
        }

        #endregion

        #region Factory

        public static Shape Create(ShapeDto dto)
        {
            var shape = new Shape
                       {
                           Name = dto.Name
                       };
            Validate(shape);
            return shape;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<Shape>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        #endregion
    }
}
