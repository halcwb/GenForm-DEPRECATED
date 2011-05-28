using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.DataAccess.Repositories
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
            var mapper = new ProductMapper();
            using (var ctx = new Informedica.GenForm.Database.GenFormDataContext(Informedica.GenForm.Database.DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm)))
            {
                var dao = new Informedica.GenForm.Database.Product();
                mapper.MapFromBoToDao(product, dao);
                ctx.Product.InsertOnSubmit(dao);

                ctx.SubmitChanges();
            }
        }

        #endregion
    }
}
