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

        #endregion

        #region Contstruction

        static ProductSubstance()
        {
            RegisterValidationRules();
        }

        protected ProductSubstance() {}

        public ProductSubstance(Product product, int sortOrder, Substance substance, decimal quantity, Unit unit)
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

        #endregion

        #region Business

        public virtual int SortOrder { get; set; }

        public virtual UnitValue Quantity { get { return _unitValue; } protected set { _unitValue = value; } }

        public virtual ISubstance Substance { get { return _substance; } protected set { _substance = (Substance)value; } }

        private void SetSubstance(Substance substance)
        {
            _substance = substance;
            _substance.AddProduct(_product);
        }

        public virtual Product Product { get { return _product; } protected set { _product = value; } }

        #endregion

        #region Factory

        public static ProductSubstance Create(ProductSubstanceDto dto, 
                                              Product product, 
                                              Substance substance, 
                                              Unit unit)
        {
            return new ProductSubstance
                       {
                           Product = product,
                           Name = dto.Name,
                           Quantity = new UnitValue(dto.Quantity, unit),
                           Substance = substance,
                           SortOrder = dto.SortOrder
                       };
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<ProductSubstance>(x => !String.IsNullOrWhiteSpace(x.Name), "Artikel stof moet een naam hebben");
            ValidationRulesManager.RegisterRule<ProductSubstance>(x => x.SortOrder > 0, "Artikel stof moet een volgorde nummer hebben");
            ValidationRulesManager.RegisterRule<ProductSubstance>(x => x._unitValue.Value > 0, "Artikel stof moet een hoeveelheid hebben");
            ValidationRulesManager.RegisterRule<ProductSubstance>(x => x._unitValue.Unit != null, "Artikel stof moet eenheid hebben");
        }

        #endregion

        internal static ProductSubstance Create(ProductSubstanceDto dto, Product product, Substance substance, UnitValue quantity)
        {
            var prodSubst = new ProductSubstance
                            {
                                Product = product,
                                Substance = substance,
                                Name = dto.Name,
                                Quantity = quantity,
                                SortOrder = dto.SortOrder
                            };

            Validate(prodSubst);
            return prodSubst;
        }

        internal static ProductSubstance Create(int sortorder, Product product, Substance substance, UnitValue quantity)
        {
            var prodSubst = new ProductSubstance
                            {
                                Name = substance.Name,
                                Product = product,
                                Quantity = quantity,
                                SortOrder = sortorder,
                                Substance = substance
                            };

            Validate(prodSubst);
            return prodSubst;
        }
    }
}