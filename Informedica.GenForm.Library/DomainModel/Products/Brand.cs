using System;
using System.Collections.Generic;
using Informedica.GenForm.DomainModel.Interfaces;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products.Collections;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Brand>, IBrand
    {
        #region Private

        private ProductSet<Brand> _products;

        #endregion

        #region Construction

        static Brand()
        {
            RegisterValidationRules();
        }

        protected Brand()
        {
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            _products = new ProductSet<Brand>(this);
        }

        #endregion

        #region Business

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
            var brand = new Brand
                       {
                           Name = brandDto.Name
                       };
            Validate(brand);
            return brand;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<Brand>(x => !String.IsNullOrWhiteSpace(x.Name), "Merk moet een naam hebben");
        }

        #endregion

        #region Overrides of Entity<Brand,Guid>

        public override bool IsIdentical(Brand entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
