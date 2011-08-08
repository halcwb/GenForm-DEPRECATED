using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : Entity<Guid, BrandDto>, IBrand
    {
        private IEnumerable<Product> _products;

        protected Brand(): base(new BrandDto()){}

        public Brand(BrandDto dto) : base(dto.CloneDto()) {}

        public virtual String Name { get { return Dto.Name; } set { Dto.Name = value; } }

        public virtual IEnumerable<Product> Products
        {
            get {
                return _products ?? (_products = new List<Product>());
            }
        }
    }

    public class BrandDto: DataTransferObject<BrandDto, Guid>
    {
        public string Name;

        public override BrandDto CloneDto()
        {
            return CreateClone();
        }
    }
}
