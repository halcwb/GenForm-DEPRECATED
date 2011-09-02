using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Tests.Fixtures
{
    public static class BrandTestFixtures
    {
        public static BrandDto GetDto()
        {
            return new BrandDto
                       {
                           Name = "Dynatra"
                       };
        }

        public static Brand GetBrandWithNoProducts()
        {
            var brand = Brand.Create(GetDto());
            return brand;
        }
    }
}