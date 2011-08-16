using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class ProductSubstance : IProductSubstance
    {
        private readonly ProductSubstanceDto _dto;
        private Substance _substance;
        private Product _product;
        private UnitValue _unitValue;

        protected ProductSubstance() { _dto = new ProductSubstanceDto(); }

        private ProductSubstance(Product product, ProductSubstanceDto productSubstanceDto)
        {
            _product = product;
            _dto = productSubstanceDto;

            SetSubstance(Substance.Create(new SubstanceDto {Name = _dto.Substance}));
            SetQuantity();
        }

        private void SetQuantity()
        {
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


        private void SetSubstance(Substance substance)
        {
            _substance = substance;
            _substance.AddProduct(_product);
        }

        #region Implementation of IProductSubstance

        public virtual Int32 Id { get; protected set; }
        public virtual int SortOrder { get { return _dto.SortOrder; } set { _dto.SortOrder = value; } }
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