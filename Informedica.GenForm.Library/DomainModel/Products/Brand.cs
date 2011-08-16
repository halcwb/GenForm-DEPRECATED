using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Guid, BrandDto>, IBrand
    {
        private readonly ISet<Product> _products = new HashSet<Product>(new ProductComparer());

        protected Brand(): base(new BrandDto()){}

        private Brand(BrandDto dto) : base(dto.CloneDto()) {}

        public virtual IEnumerable<Product> Products
        {
            get {
                return _products;
            }
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public static Brand Create(BrandDto brandDto)
        {
            return new Brand(brandDto);
        }

        internal protected virtual void AddProduct(Product product)
        {
            product.SetBrand(this, AddProductToBrand);
        }

        private void AddProductToBrand(Product product)
        {
            _products.Add(product);
        }

        internal protected virtual void RemoveProduct(Product product)
        {
            _products.Remove(product);
        }
    }
}
