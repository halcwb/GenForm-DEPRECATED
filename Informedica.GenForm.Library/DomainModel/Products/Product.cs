using System;
using System.Collections.Generic;
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
        private UnitValue _unitValue;
        private Brand _brand;
        private Package _package;
        private Shape _shape;
        private ProductDto _dto;
        private RouteSet<Product> _routes;

        #endregion

        #region Constructor

        static Product()
        {
            RegisterValidationRules();
        }
        
        protected Product()
        {
            _dto = new ProductDto();
            InitializeCollections();
        }

        private Product(ProductDto dto)
        {
            ValidateDto(dto);
            InitializeCollections();
            InitializeProduct();
        }

        private void InitializeProduct()
        {
            AddProductToBrand();
            AddProductToShape();
            AddProductToPackage();
            if (Shape != null) Shape.AddPackage(Package);

            SetQuantity();
            if (Shape != null) Shape.AddUnitGroup(Quantity.Unit.UnitGroup);

            foreach (var substanceDto in _dto.Substances)
            {
                GetSubstances().Add(CreateSubstance(substanceDto));
            }

            foreach (var dto in _dto.Routes)
            {
                AddRoute(Route.Create(dto));
            }
        }

        private void InitializeCollections()
        {
            _routes = new RouteSet<Product>(this);
        }

        private ProductSubstance CreateSubstance(ProductSubstanceDto productSubstanceDto)
        {
            return ProductSubstance.Create(this, productSubstanceDto);
        }

        private void AddProductToPackage()
        {
            if (String.IsNullOrWhiteSpace(_dto.ShapeName)) return;
            var package = Package.Create(new PackageDto { Abbreviation = _dto.PackageName, Name = _dto.PackageName });
            SetPackage(package);
        }

        private void AddProductToShape()
        {
            if (String.IsNullOrWhiteSpace(_dto.ShapeName)) return;
            var shape = Shape.Create(new ShapeDto { Name = _dto.ShapeName });
            SetShape(shape);
        }

        private void AddProductToBrand()
        {
            if (String.IsNullOrWhiteSpace(_dto.BrandName)) return;

            var brand = Brand.Create(new BrandDto { Name = _dto.BrandName });
            SetBrand(brand);
        }

        private void SetQuantity()
        {
            if (String.IsNullOrWhiteSpace(_dto.UnitName)) return;

            _unitValue = UnitValue.Create(_dto.Quantity, Unit.Create(new UnitDto
            {
                Abbreviation = _dto.UnitAbbreviation,
                Name = _dto.UnitName,
                AllowConversion = _dto.UnitGroupAllowConversion,
                Divisor = _dto.UnitDivisor,
                IsReference = _dto.UnitIsReference,
                Multiplier = _dto.UnitMultiplier,
                UnitGroupName = _dto.UnitGroupName
            }));
        }

        #endregion
        
        #region Business

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        public virtual string DisplayName { get { return _dto.DisplayName ?? _dto.Name; } protected set { _dto.DisplayName = value; } }

        public virtual string ProductCode { get { return _dto.ProductCode; } protected set { _dto.ProductCode = value; } }

        public virtual string GenericName { get { return _dto.GenericName; } protected set { _dto.GenericName = value; } }

        public virtual UnitValue Quantity { get { return _unitValue; } protected set { _unitValue = value; } }

        private void SetUnitValue(UnitValue unitValue)
        {
            _unitValue = unitValue;
        }

        public virtual Brand Brand
        {
            get { return _brand; }
            protected set { _brand = value; }
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

        public virtual Package Package
        {
            get { return _package; } 
            protected set { _package = value; }
        }

        public virtual void SetPackage(Package package)
        {
            if (_package == package) return;

            if (_package != null) _package.RemoveProduct(this);
            _package = package;
            package.AddProduct(this);
        }

        public virtual Shape Shape
        {
            get { return _shape; } 
            protected set { _shape = value; }
        }

        public virtual void SetShape(Shape shape)
        {
            if (_shape == shape) return;

            if (_shape != null) _shape.RemoveProduct(this);
            _shape = shape;
            shape.AddProduct(this);
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

        public virtual IList<ProductSubstance> SubstanceList
        {
            get
            {
                return GetSubstances();
            }

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
            GetSubstances().Add(substance);
        }

        #endregion

        #region Factory

        public static Product Create(ProductDto dto)
        {
            return new Product(dto);
        }

        public static Product Create(ProductDto dto, Shape shape, Package package, UnitValue unitValue)
        {
            var product = new Product(dto);
            product.SetShape(shape);
            product.SetPackage(package);
            product.SetUnitValue(unitValue);
            return product;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<ProductDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<ProductDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();

        }

        #endregion

    }
}