using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.IoC;
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
            var mapper = GetMapper();
            using (var ctx = GetDataContext())
            {
                var dao = new Database.Product();
                mapper.MapFromBoToDao(product, dao);
                ctx.Product.InsertOnSubmit(dao);

                ctx.SubmitChanges();
            }
        }

        private static ProductMapper GetMapper()
        {
            return ObjectFactory.GetInstanceFor<ProductMapper>();
        }

        private static GenFormDataContext GetDataContext()
        {
            return ObjectFactory.With(DatabaseConnection.DatabaseName.GenForm).GetInstance<GenFormDataContext>();
        }

        #endregion
    }
}
