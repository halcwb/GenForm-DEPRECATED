using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.Factories
{
    public class ProductFactory
    {
        public static IProduct CreateProduct(ProductDto dto)
        {
            return new Product(dto);
        }
    }
}
