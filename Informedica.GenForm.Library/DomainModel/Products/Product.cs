using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : Entity<Guid, ProductDto>, IProduct, IRelationPart
    {
        #region Private Fields
        
        private IList<ProductSubstance> _substances;
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
            if (Shape != null) Shape.AddPackage(Package);

            SetQuantity();
            if (Shape != null) Shape.AddUnitGroup(Quantity.Unit.UnitGroup);

            foreach (var substanceDto in Dto.Substances)
            {
                GetSubstances().Add(CreateSubstance(substanceDto));
            }

            foreach (var dto in Dto.Routes)
            {
                RelationProvider.RouteProduct.Add(Route.Create(dto), this);
            }
        }

        private void AddProductToPackage()
        {
            if (String.IsNullOrWhiteSpace(Dto.ShapeName)) return;
            var package = Package.Create(new PackageDto {Abbreviation = Dto.PackageName, Name = Dto.PackageName});
            RelationProvider.PackageProduct.Add(package, this);
        }

        private void AddProductToShape()
        {
            if (String.IsNullOrWhiteSpace(Dto.ShapeName)) return;
            var shape = Shape.Create(new ShapeDto {Name = Dto.ShapeName});
            RelationProvider.ShapeProduct.Add(shape, this);
        }

        private void AddProductToBrand()
        {
            if (!String.IsNullOrWhiteSpace(Dto.BrandName))
            {
                var brand = Brand.Create(new BrandDto {Name = Dto.BrandName});
                RelationProvider.BrandProduct.Add(brand, this);
            }
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
            get { return RelationProvider.BrandProduct.GetOnePart(this); } 
            set { RelationProvider.BrandProduct.Add(value, this); }
        }

        public virtual Package Package
        {
            get { return RelationProvider.PackageProduct.GetOnePart(this); } 
            protected set { RelationProvider.PackageProduct.Add(value, this); }
        }

        public virtual Shape Shape
        {
            get { return RelationProvider.ShapeProduct.GetOnePart(this); } 
            protected set { RelationProvider.ShapeProduct.Add(value, this); }
        }

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
            RelationProvider.RouteProduct.Add(route, this);
        }

        public virtual IEnumerable<Route> Routes
        {
            get { return RelationProvider.RouteProduct.GetManyPartLeft(this); }  
            protected set { RelationProvider.RouteProduct.Add(value, this); }
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

        public virtual void AddBrand(Brand brand)
        {
            RelationProvider.BrandProduct.Add(brand, this);
        }

        public static Product Create(ProductDto dto, Shape shape, Package package, UnitValue unitValue)
        {
            var product = new Product(dto);
            RelationProvider.ShapeProduct.Add(shape, product);
            RelationProvider.PackageProduct.Add(package, product);
            product.AddUnitValue(unitValue);
            return product;
        }

        private void AddUnitValue(UnitValue unitValue)
        {
            _unitValue = unitValue;
        }

        public virtual void AddSubstance(int sortOrder, Substance substance, Decimal quantity, Unit unit)
        {
            GetSubstances().Add(new ProductSubstance(this, sortOrder, substance, quantity, unit));
        }

        public virtual void RemoveRoute(Route route)
        {
            RelationProvider.RouteProduct.Remove(route, this);
        }
    }
}