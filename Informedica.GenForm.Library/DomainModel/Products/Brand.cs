using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Brand>, IBrand
    {
        #region Private

        private BrandDto _dto;
        private ProductSet<Brand> _products;

        #endregion

        #region Construction

        static Brand()
        {
            RegisterValidationRules();
        }

        protected Brand()
        {
            _dto = new BrandDto();
            InitializeCollections();
        }

        private Brand(BrandDto dto)
        {
            ValidateDto(dto);
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            _products = new ProductSet<Brand>(this);
        }

        #endregion

        #region Business

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        public virtual IEnumerable<IProduct> Products
        {
            get { return _products; }
        }
        
        public virtual Iesi.Collections.Generic.ISet<Product> ProductSet
        {
            get { return _products.GetEntitySet(); }
            protected set { _products = new ProductSet<Brand>(value, this); }
        }

        public virtual void AddProduct(IProduct product)
        {
            _products.Add((Product)product, ((Product)product).SetBrand);
        }

        public virtual void RemoveProduct(IProduct product)
        {
            _products.Remove((Product)product, ((Product)product).RemoveFromBrand);
        }

        public virtual void RemoveAllProducts()
        {
            var list = new List<Product>(ProductSet);
            foreach (var product in list)
            {
                product.RemoveFromBrand();
            }
        }

        internal protected virtual void ChangeName(string obj)
        {
            Name = obj;
        }

        #endregion

        #region Factory

        public static Brand Create(BrandDto brandDto)
        {
            return new Brand(brandDto);
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<ProductSubstanceDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<BrandDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        #endregion

    }
}
