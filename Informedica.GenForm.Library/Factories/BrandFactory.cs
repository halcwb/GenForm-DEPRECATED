using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class BrandFactory : EntityFactory<Brand, BrandDto>
    {
        public BrandFactory(BrandDto dto) : base(dto) {}

        protected override Brand Create()
        {
            var brand = Brand.Create(Dto);
            Add(brand);
            return brand;
        }
    }
}