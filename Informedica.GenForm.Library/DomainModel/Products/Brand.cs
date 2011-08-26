using System;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Guid, BrandDto>, IBrand
    {
        private Iesi.Collections.Generic.ISet<Product> _products = new HashedSet<Product>();
        private readonly ProductComparer  _productComparer = new ProductComparer();

        protected Brand(): base(new BrandDto()){}

        private Brand(BrandDto dto) : base(dto.CloneDto()) {}

        public virtual Iesi.Collections.Generic.ISet<Product> Products
        {
            get  { return _products; }
            protected set { _products = value;}
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
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
    }
}
