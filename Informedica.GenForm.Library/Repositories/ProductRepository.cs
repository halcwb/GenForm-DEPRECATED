using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Repositories
{
    public class ProductRepository: IProductRepository
    {
        #region Implementation of IRepository<IProduct>

        public IEnumerable<IProduct> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProduct> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void SaveProduct(IProduct product)
        {
            throw  new NotImplementedException();
        }

        #endregion
    }
}
