using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class ProductSubstance : Entity<ProductSubstance>, IProductSubstance
    {
        #region Private

        private Substance _substance;
        private Product _product;
        private UnitValue _unitValue;
        private ProductSubstanceDto _dto;

        #endregion

        #region Contstruction

        static ProductSubstance()
        {
            RegisterValidationRules();
        }

        protected ProductSubstance()
        {
            _dto = new ProductSubstanceDto();
        }

        private ProductSubstance(Product product, ProductSubstanceDto dto)
        {
            ValidateDto(dto);

            if (product != null) _product = product;

            SetSubstance(Substance.Create(new SubstanceDto { Name = _dto.Substance }));
            SetQuantity();
        }

        public ProductSubstance(Product product, int sortOrder, Substance substance, decimal quantity, Unit unit)
            : base()
        {
            _dto = new ProductSubstanceDto();
            Initialize(product, sortOrder, substance, quantity, unit);
        }

        private void Initialize(Product product, int sortOrder, Substance substance, decimal quantity, Unit unit)
        {
            if (product != null) Product = product;
            SortOrder = sortOrder;
            SetSubstance(substance);
            _unitValue = UnitValue.Create(quantity, unit);
        }

        #endregion

        #region Business

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        public virtual int SortOrder { get { return _dto.SortOrder; } set { _dto.SortOrder = value; } }

        public virtual UnitValue Quantity { get { return _unitValue; } protected set { _unitValue = value; } }

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

        public virtual Substance Substance { get { return _substance; } protected set { _substance = value; } }

        private void SetSubstance(Substance substance)
        {
            _substance = substance;
            _substance.AddProduct(_product);
        }

        public virtual Product Product { get { return _product; } protected set { _product = value; } }

        #endregion

        #region Factory

        public static ProductSubstance Create(Product product, ProductSubstanceDto dto)
        {
            return new ProductSubstance(product, dto);
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<ProductSubstanceDto>(x => !String.IsNullOrWhiteSpace(x.Name));
            ValidationRulesManager.RegisterRule<ProductSubstanceDto>(x => x.SortOrder > 0);
            ValidationRulesManager.RegisterRule<ProductSubstanceDto>(x => x.Quantity > 0);
            ValidationRulesManager.RegisterRule<ProductSubstanceDto>(x => !String.IsNullOrWhiteSpace(x.Substance));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<ProductSubstanceDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        #endregion
    
    }
}