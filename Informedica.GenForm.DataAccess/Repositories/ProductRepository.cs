using System;
using System.Collections.Generic;
using System.Data;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using StructureMap;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
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
                ctx.Connection.Open();
                try
                {
                    using (var transaction = ctx.Connection.BeginTransaction(IsolationLevel.Unspecified))
                    {
                        ctx.Transaction = transaction;
                        var dao = GetProductDao();
                        mapper.MapFromBoToDao(product, dao);
                        ctx.Product.InsertOnSubmit(dao);

                        try
                        {
                            ctx.SubmitChanges();
                        }
                        catch(Exception e)
                        {
                            throw;
                        }
                        finally
                        {
                            transaction.Rollback();
                        }
                    }

                }
                finally
                {
                    ctx.Connection.Close();
                }
            }
        }

        private Product GetProductDao()
        {
            return ObjectFactory.GetInstance<Product>();
        }

        private static ProductMapper GetMapper()
        {
            return ObjectFactory.GetInstance<ProductMapper>();
        }

        private static GenFormDataContext GetDataContext()
        {
            var connection = GetConnectionString();
            return ObjectFactory.With<String>(connection).GetInstance<GenFormDataContext>();
        }

        private static String GetConnectionString()
        {
            return DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm);
        }

        #endregion
    }
}
