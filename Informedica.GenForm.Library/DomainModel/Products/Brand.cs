using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Guid, BrandDto>, IBrand, IRelationPart
    {
        protected Brand(): base(new BrandDto()){}

        private Brand(BrandDto dto) : base(dto.CloneDto()) {}

        public virtual IEnumerable<Product> Products
        {
            get 
            {
                return RelationProvider.BrandProduct.GetManyPart(this);
            }
            protected set { RelationProvider.BrandProduct.Add(this, new HashSet<Product>(value));}
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public static Brand Create(BrandDto brandDto)
        {
            return new Brand(brandDto);
        }
    }
}
