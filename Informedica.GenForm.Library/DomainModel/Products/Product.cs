using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : Entity<Product>, IProduct
    {
        #region Private Fields

        private IList<ProductSubstance> _substances = new List<ProductSubstance>();
        private Brand _brand;
        private Package _package;
        private Shape _shape;
        private RouteSet<Product> _routes;

        #endregion

        #region Constructor

        static Product()
        {
            RegisterValidationRules();
        }

        protected Product()
        {
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            _routes = new RouteSet<Product>(this);
        }

        private void SetUnitValue(UnitValue unitValue)
        {
            Quantity = unitValue;
        }

        #endregion

        #region Business

        public virtual string DisplayName { get; set; }

        public virtual string ProductCode { get; set; }
        
        public virtual string GenericName { get; set; }

        public virtual UnitValue Quantity { get; set; }

        public virtual IBrand Brand
        {
            get { return _brand; }
            protected set { _brand = (Brand)value; }
        }

        public virtual void SetBrand(Brand brand)
        {
            if (_brand == brand) return;

            if (_brand != null) _brand.RemoveProduct(this);
            _brand = brand;
            if (brand != null) brand.AddProduct(this);
        }

        public virtual void RemoveFromBrand()
        {
            SetBrand(null);
        }

        public virtual IPackage Package
        {
            get { return _package; }
            protected set { _package = (Package)value; }
        }

        public virtual void SetPackage(Package package)
        {
            if (_package == package) return;

            if (_package != null) _package.RemoveProduct(this);
            _package = package;
            package.AddProduct(this);
        }

        public virtual IShape Shape
        {
            get { return _shape; }
            protected set { _shape = (Shape)value; }
        }

        public virtual void SetShape(Shape shape)
        {
            if (_shape == shape) return;

            if (_shape != null) _shape.RemoveProduct(this);
            _shape = shape;
            shape.AddProduct(this);
        }

        public virtual void RemoveSubstance(ISubstance substance)
        {
            var _prodSubst = _substances.SingleOrDefault(x => x.Substance == substance);
            if (_prodSubst == null) return;

            _substances.Remove(_prodSubst);
        }

        public virtual IEnumerable<IRoute> Routes
        {
            get { return _routes; }
        }

        public virtual Iesi.Collections.Generic.ISet<Route> RouteSet
        {
            get { return _routes.GetEntitySet(); }
            protected set { _routes = new RouteSet<Product>(value, this); }
        }

        public virtual bool ContainsRoute(IRoute route)
        {
            return _routes.Contains((Route)route);
        }

        public virtual void AddRoute(IRoute route)
        {
            _routes.Add((Route)route, ((Route)route).AddProduct);
        }

        public virtual void RemoveRoute(IRoute route)
        {
            _routes.Remove((Route)route, ((Route)route).RemoveProduct);
        }

        public virtual IEnumerable<IProductSubstance> Substances
        {
            get { return GetSubstances(); }
        }

        public virtual bool ContainsSubstance(ISubstance substance)
        {
            return _substances.Count(x => x.Substance == substance) > 0;
        }

        public virtual void AddSubstance(ISubstance substance, int sortOrder, UnitValue quanity)
        {
            if (ContainsSubstance(substance)) return;
            
            _substances.Add(ProductSubstance.Create(sortOrder, this, (Substance)substance, quanity));
        }

        public virtual IList<ProductSubstance> SubstanceList
        {
            get { return GetSubstances(); }

            protected set { _substances = value; }
        }

        private IList<ProductSubstance> GetSubstances()
        {
            return _substances ?? (_substances = new List<ProductSubstance>());
        }

        public virtual void AddSubstance(int sortOrder, Substance substance, Decimal quantity, Unit unit)
        {
            GetSubstances().Add(new ProductSubstance(this, sortOrder, substance, quantity, unit));
        }

        public virtual void AddSubstance(ProductSubstance substance)
        {
            if (GetSubstances().Contains(substance)) return;

            GetSubstances().Add(substance);
            substance.Substance.AddProduct(this);
        }

        #endregion

        #region Factory

        public static ProductShapeCreate Create(ProductDto dto)
        {
            var product = new Product
                              {
                                  Name = dto.Name,
                                  DisplayName = dto.DisplayName,
                                  ProductCode = dto.ProductCode,
                                  GenericName = dto.GenericName
                              };

            return new ProductShapeCreate(product);
        }

        public class ProductShapeCreate
        {
            private Product _product;

            internal ProductShapeCreate(Product product)
            {
                _product = product;
            }

            public ProductPackageCreate Shape(Shape shape)
            {
                _product.SetShape(shape);
                var product = _product;
                _product = null;
                return new ProductPackageCreate(product);
            }
        }

        public class ProductPackageCreate
        {
            private Product _product;

            internal ProductPackageCreate(Product product)
            {
                _product = product;
            }

            public ProductUnitValueCreate Package(Package package)
            {
                _product.SetPackage(package);
                var product = _product;
                _product = null;
                return new ProductUnitValueCreate(product);
            }
        }

        public class ProductUnitValueCreate
        {
            private Product _product;

            internal ProductUnitValueCreate(Product product)
            {
                _product = product;
            }

            public ProductSubstanceCreate Quantity(Unit unit, Decimal quantity)
            {
                var qty = new UnitValue(quantity, unit);
                _product.SetUnitValue(qty);
                var product = _product;
                _product = null;
                return new ProductSubstanceCreate(product);
            }
        }

        public class ProductSubstanceCreate
        {
            private Product _product;

            internal ProductSubstanceCreate(Product product)
            {
                _product = product;
            }

            public ProductRouteCreate Substance(int sortorder, Substance substance, decimal quantity, Unit unit)
            {
                var qty = new UnitValue(quantity, unit);
                var subst = ProductSubstance.Create(sortorder, _product, substance, qty);
                _product.AddSubstance(subst);

                var product = _product;
                _product = null;
                return new ProductRouteCreate(product);
            }
        }

        public class ProductRouteCreate
        {
            private Product _product;

            internal ProductRouteCreate(Product product)
            {
                _product = product;
            }

            public Product Route(Route route)
            {
                _product.AddRoute(route);
                var product = _product;
                _product = null;

                Validate(product);
                return product;
            }
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<Product>(x => !String.IsNullOrWhiteSpace(x.DisplayName),
                                                         "Product moet een naam hebben");
            ValidationRulesManager.RegisterRule<Product>(x => !String.IsNullOrWhiteSpace(x.GenericName),
                                                         "Product moet een generiek naam hebben");
            ValidationRulesManager.RegisterRule<Product>(x => x.Shape != null, "Product moet een vorm hebben");
            ValidationRulesManager.RegisterRule<Product>(x => x.Package != null, "Product moet een verpakking hebben");
            ValidationRulesManager.RegisterRule<Product>(x => x.Quantity.Value > 0, "Product moet een hoeveelheid hebben");
            ValidationRulesManager.RegisterRule<Product>(x => x.Quantity.Unit != null, "Product moet een eenheid hebben");
        }

        #endregion

    }
}