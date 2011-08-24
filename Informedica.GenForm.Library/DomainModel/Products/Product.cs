using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : Entity<Guid, ProductDto>, IProduct, IRelationPart
    {
        #region Private Fields
        
        private IList<ProductSubstance> _substances = new List<ProductSubstance>();
        private UnitValue _unitValue;
        private Brand _brand;
        private Package _package;
        private Shape _shape;
        private Iesi.Collections.Generic.ISet<Route> _routes = new HashedSet<Route>();

        #endregion

        #region Constructor
        
        protected Product() : base(new ProductDto()) { }

        private Product(ProductDto dto) : base(dto.CloneDto())
        {
            Initialize();
        }

        private void Initialize()
        {
            AddProductToBrand();
            AddProductToShape();
            AddProductToPackage();
            if (Shape != null) Shape.AddPackage(Package);

            SetQuantity();
            if (Shape != null) Shape.AddUnitGroup(Quantity.Unit.UnitGroup);

            foreach (var substanceDto in Dto.Substances)
            {
                GetSubstances().Add(CreateSubstance(substanceDto));
            }

            foreach (var dto in Dto.Routes)
            {
                AddRoute(Route.Create(dto));
            }
        }

        private void AddProductToPackage()
        {
            if (String.IsNullOrWhiteSpace(Dto.ShapeName)) return;
            var package = Package.Create(new PackageDto {Abbreviation = Dto.PackageName, Name = Dto.PackageName});
            SetPackage(package);
        }

        public virtual void SetPackage(Package package)
        {
            if (_package == package) return;

            if (_package != null) _package.RemoveProduct(this);
            _package = package;
            package.AddProduct(this);
        }

        private void AddProductToShape()
        {
            if (String.IsNullOrWhiteSpace(Dto.ShapeName)) return;
            var shape = Shape.Create(new ShapeDto {Name = Dto.ShapeName});
            SetShape(shape);
        }

        public virtual void SetShape(Shape shape)
        {
            if (_shape == shape) return;

            if (_shape != null) _shape.RemoveProduct(this);
            _shape = shape;
            shape.AddProduct(this);
        }

        private void AddProductToBrand()
        {
            if (String.IsNullOrWhiteSpace(Dto.BrandName)) return;
            
            var brand = Brand.Create(new BrandDto {Name = Dto.BrandName});
            SetBrand(brand);
        }

        private void SetQuantity()
        {
            if (String.IsNullOrWhiteSpace(Dto.UnitName)) return;

            _unitValue = UnitValue.Create(Dto.Quantity, Unit.Create(new UnitDto
            {
                Abbreviation = Dto.UnitAbbreviation,
                Name = Dto.UnitName,
                AllowConversion = Dto.UnitGroupAllowConversion,
                Divisor = Dto.UnitDivisor,
                IsReference = Dto.UnitIsReference,
                Multiplier = Dto.UnitMultiplier,
                UnitGroupName = Dto.UnitGroupName
            }));
        }

        private ProductSubstance CreateSubstance(ProductSubstanceDto productSubstanceDto)
        {
            return ProductSubstance.Create(this, productSubstanceDto);
        }

        #endregion
        
        #region Implementation of IProduct

        public virtual string ProductCode { get { return Dto.ProductCode; } protected set { Dto.ProductCode = value; } }

        public virtual string GenericName { get { return Dto.GenericName; } protected set { Dto.GenericName = value; } }

        public virtual UnitValue Quantity { get { return _unitValue; } protected set { _unitValue = value; } }

        public virtual string DisplayName { get { return Dto.DisplayName ?? Dto.Name; } protected set { Dto.DisplayName = value; } }

        public virtual Brand Brand
        {
            get { return _brand; }
            protected set { _brand = value; }
        }

        public virtual Package Package
        {
            get { return _package; } 
            protected set { _package = value; }
        }

        public virtual Shape Shape
        {
            get { return _shape; } 
            protected set { _shape = value; }
        }

        private IList<ProductSubstance> GetSubstances()
        {
            return _substances ?? (_substances = new List<ProductSubstance>());
        }

        public virtual IList<ProductSubstance> Substances
        {
            get
            {
                return GetSubstances();
            }

            protected set { _substances = value; }
        }

        public virtual void AddRoute(Route route)
        {
            if (_routes.Contains(route)) return;

            _routes.Add(route);
            route.AddProduct(this);
        }

        public virtual Iesi.Collections.Generic.ISet<Route> Routes
        {
            get { return _routes; }  
            protected set { _routes = value; }
        }

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public static Product Create(ProductDto dto)
        {
            return new Product(dto);
        }

        public virtual void SetBrand(Brand brand)
        {
            if (_brand == brand) return;
            
            if (_brand != null) _brand.RemoveProduct(this);
            _brand = brand;
            if (brand != null) brand.AddProduct(this);
        }

        public static Product Create(ProductDto dto, Shape shape, Package package, UnitValue unitValue)
        {
            var product = new Product(dto);
            product.SetShape(shape);
            product.SetPackage(package);
            product.SetUnitValue(unitValue);
            return product;
        }

        private void SetUnitValue(UnitValue unitValue)
        {
            _unitValue = unitValue;
        }

        public virtual void AddSubstance(int sortOrder, Substance substance, Decimal quantity, Unit unit)
        {
            GetSubstances().Add(new ProductSubstance(this, sortOrder, substance, quantity, unit));
        }

        public virtual void RemoveRoute(Route route)
        {
            if (!_routes.Contains(route)) return;

            _routes.Remove(route);
            route.RemoveProduct(this);
        }
    }
}