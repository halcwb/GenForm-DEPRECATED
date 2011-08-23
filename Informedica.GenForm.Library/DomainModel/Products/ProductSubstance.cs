using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class ProductSubstance : Entity<Guid, ProductSubstanceDto>, IProductSubstance
    {
        private Substance _substance;
        private Product _product;
        private UnitValue _unitValue;

        protected ProductSubstance(): base (new ProductSubstanceDto()) {}

        private ProductSubstance(Product product, ProductSubstanceDto dto) : base(dto)
        {
            if (product != null) _product = product;

            SetSubstance(Substance.Create(new SubstanceDto {Name = Dto.Substance}));
            SetQuantity();
        }

        public ProductSubstance(Product product, int sortOrder, Substance substance, decimal quantity, Unit unit) : base(new ProductSubstanceDto())
        {
            Initialize(product, sortOrder, substance, quantity, unit);
        }

        private void Initialize(Product product, int sortOrder, Substance substance, decimal quantity, Unit unit)
        {
            if (product != null) Product = product;
            SortOrder = sortOrder;
            SetSubstance(substance);
            _unitValue = UnitValue.Create(quantity, unit);
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


        private void SetSubstance(Substance substance)
        {
            _substance = substance;
            _substance.AddProduct(_product);
        }

        #region Implementation of IProductSubstance

        public override bool IdIsDefault(Guid id)
        {
            return id.Equals(Guid.Empty);
        }

        public virtual int SortOrder { get { return Dto.SortOrder; } set { Dto.SortOrder = value; } }
        public virtual Substance Substance { get { return _substance; } protected set { _substance = value; } }
        public virtual UnitValue Quantity { get { return _unitValue; } protected set { _unitValue = value; } }

        public virtual Product Product { get { return _product; } protected set { _product = value; } }

        #endregion

        public static ProductSubstance Create(Product product, ProductSubstanceDto dto)
        {
            return new ProductSubstance(product, dto);
        }
    }
}