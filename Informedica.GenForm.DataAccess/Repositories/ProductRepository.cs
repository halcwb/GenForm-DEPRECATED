using System;
using System.Collections.Generic;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class ProductRepository: Repository<IProduct, Product>, IProductRepository
    {
        #region Implementation of IRepository<IProduct>

        public override IEnumerable<IProduct> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IProduct> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        public override void Insert(IProduct item)
        {
            InsertUsingMapper<IDataMapper<IProduct, Product>>(item);
        }

        protected override void InsertOnSubmit(GenFormDataContext ctx, Product dao)
        {
            ctx.Product.InsertOnSubmit(dao);
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IProduct item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
