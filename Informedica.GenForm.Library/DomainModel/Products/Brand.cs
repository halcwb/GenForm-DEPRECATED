using System;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Guid, BrandDto>, IBrand, IRelationPart
    {
        private ISet<Product> _products = new HashedSet<Product>();
        protected Brand(): base(new BrandDto()){}

        private Brand(BrandDto dto) : base(dto.CloneDto()) {}

        public virtual ISet<Product> Products
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
            if (_products.Contains(product))
            {
                _products.Remove(product);
            }
        }

        public virtual void AddProduct(Product product)
        {
            if (_products.Contains(product)) return;

            _products.Add(product);
            product.SetBrand(this);
        }
    }
}
