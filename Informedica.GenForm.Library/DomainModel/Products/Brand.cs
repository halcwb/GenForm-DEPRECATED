using System;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Brand>
    {
        private Iesi.Collections.Generic.ISet<Product> _products = new HashedSet<Product>();
        private readonly ProductComparer  _productComparer = new ProductComparer();
        private BrandDto _dto;

        static Brand()
        {
            RegisterValidationRules();
        }

        protected Brand(): base(new BrandComparer())
        {
            _dto = new BrandDto();
        }

        private Brand(BrandDto dto) : base(new BrandComparer())
        {
            ValidateDto(dto);
        }

        public virtual Iesi.Collections.Generic.ISet<Product> Products
        {
            get  { return _products; }
            protected set { _products = value;}
        }

        public static Brand Create(BrandDto brandDto)
        {
            return new Brand(brandDto);
        }

        internal protected virtual void RemoveProduct(Product product)
        {
            if (!ContainsProduct(product)) return;

            _products.Remove(product);
        }

        public virtual bool ContainsProduct(Product product)
        {
            return _products.Contains(product, _productComparer);
        }

        public virtual void AddProduct(Product product)
        {
            if (ContainsProduct(product)) return;

            _products.Add(product);
            product.SetBrand(this);
        }

        public virtual void RemoveAllProducts()
        {
            var list = new List<Product>(Products);
            foreach (var product in list)
            {
                product.RemoveFromBrand();
            }
        }

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<ProductSubstanceDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<BrandDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        internal protected virtual void ChangeName(string obj)
        {
            Name = obj;
        }
    }
}
