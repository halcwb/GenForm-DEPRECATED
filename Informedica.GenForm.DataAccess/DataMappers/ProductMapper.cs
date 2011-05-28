using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.DomainModel.Products;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.DataMappers
{
    public class ProductMapper: IDataMapper<IProduct, Product>
    {
        #region Implementation of IDataMapper<IProduct,Product>

        public void MapFromBoToDao(IProduct bo, Product dao)
        {
            throw new NotImplementedException();
        }

        public void MapFromDaoToBo(Product dao, IProduct bo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
