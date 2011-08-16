using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Exceptions;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : Entity<Guid, ProductDto>, IProduct
    {
        #region Private Fields
        
        private IList<ProductSubstance> _substances;
        private HashSet<Route> _routes;
        private Brand _brand;
        private Shape _shape;
        private Package _package;
        private UnitValue _unitValue;

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
            Shape.AddPackage(Package);

            SetQuantity();
            Shape.AddUnit(Quantity.Unit);

            foreach (var substanceDto in Dto.Substances)
            {
                GetSubstances().Add(CreateSubstance(substanceDto));
            }

            foreach (var dto in Dto.Routes)
            {
                GetRoutes().Add(CreateRoute(dto));
            }
        }

        private Route CreateRoute(RouteDto dto)
        {
            var route = Route.Create(dto);
            route.AddProduct(this);
            return route;
        }

        private HashSet<Route> GetRoutes()
        {
            return _routes ?? (_routes = new HashSet<Route>(new RouteComparer()));
        }

        private void AddProductToPackage()
        {
            Package.Create(new PackageDto {Abbreviation = Dto.PackageName, Name = Dto.PackageName}).AddProduct(this);
        }

        private void AddProductToShape()
        {
            Shape.Create(new ShapeDto {Name = Dto.ShapeName}).AddProduct(this);
        }

        private void AddProductToBrand()
        {
            if (!String.IsNullOrWhiteSpace(Dto.BrandName))
                Brand.Create(new BrandDto {Name = Dto.BrandName}).AddProduct(this);
        }

        private void SetQuantity()
        {
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

        public virtual Brand Brand { get { return _brand; } protected set { _brand = value; } }

        public virtual Package Package { get { return _package; } protected set { _package = value; } }

        public virtual Shape Shape { get { return _shape; } protected set { _shape = value; } }

        private IList<ProductSubstance> GetSubstances()
        {
            return _substances ?? (_substances = new List<ProductSubstance>());
        }

        public virtual IEnumerable<ProductSubstance> Substances
        {
            get
            {
                return GetSubstances();
            }

            protected set { _substances = value.ToList(); }
        }

        public virtual void AddRoute(Route route)
        {
            if (_routes.Contains(route, new RouteComparer())) throw new CannotAddItemException<Route>(route);
            _routes.Add(route);
            route.AddProduct(this);
        }

        public virtual IEnumerable<Route> Routes
        {
            get { return _routes ?? (_routes = new HashSet<Route>(new RouteComparer())); }  
            protected set { _routes = new HashSet<Route>(value); }
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

        internal protected virtual void SetBrand(Brand brand, Action<Product> addProductToBrand)
        {
            if (_brand != null) _brand.RemoveProduct(this);
            _brand = brand;
            addProductToBrand(this);
        }

        internal protected virtual void SetShape(Shape shape, Action<Product> addProductToShape)
        {
            if (_shape != null) _shape.Remove(this);
            _shape = shape;
            addProductToShape(this);
        }

        internal protected virtual void SetPackage(Package package, Action<Product> addProductToPackage)
        {
            if (_package != null) _package.Remove(this);
            _package = package;
            addProductToPackage(this);
        }
    }
}