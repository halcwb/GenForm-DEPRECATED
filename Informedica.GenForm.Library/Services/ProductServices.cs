using System;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Services
{
    public class ProductServices : IProductServices
    {
        #region Implementation of IProductServices

        public IProduct GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IProduct SaveProduct(IProduct product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IProduct GetEmptyProduct()
        {
            return new Product();
        }

        #endregion
    }
}