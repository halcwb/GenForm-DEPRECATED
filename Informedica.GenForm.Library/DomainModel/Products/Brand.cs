﻿using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Guid, BrandDto>, IBrand
    {
        private IEnumerable<Product> _products;

        protected Brand(): base(new BrandDto()){}

        public Brand(BrandDto dto) : base(dto.CloneDto()) {}

        public virtual IEnumerable<Product> Products
        {
            get {
                return _products ?? (_products = new List<Product>());
            }
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }
    }

    public class BrandDto: DataTransferObject<BrandDto, Guid>
    {
        public override BrandDto CloneDto()
        {
            return CreateClone();
        }
    }
}
