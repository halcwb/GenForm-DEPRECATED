using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class ProductRepository: Repository<IProduct, Product>, IProductRepository
    {
        public ProductRepository()
        {}

        public ProductRepository(GenFormDataContext ctx): base(ctx) 
        {}
 
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

        public void Insert(GenFormDataContext context, IProduct item)
        {
            InsertUsingContext<IDataMapper<IProduct, Product>>(context, item);
        }

        protected override void UpdateBo(IProduct item, Product dao)
        {
            item.ProductId = dao.ProductId;
        }

        protected override void InsertOnSubmit(GenFormDataContext ctx, Product dao)
        {
            ctx.Product.InsertOnSubmit(dao);
        }

        public override void Delete(int id)
        {
            using (var ctx = GetDataContext())
            {
                var dao = ctx.Product.Single(p => p.ProductId == id);
                ctx.Product.DeleteOnSubmit(dao);
            }
        }

        public override void Delete(IProduct item)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IProduct> Fetch(GenFormDataContext context, Func<Product, Boolean> selector)
        {
            var list = context.Product.Where(selector);
            return CreateProductListFromDaoList(list);
        }

        private IEnumerable<IProduct> CreateProductListFromDaoList(IEnumerable<Product> list)
        {
            IList<IProduct> productList = new List<IProduct>();
            var mapper = GetMapper<IDataMapper<IProduct, Product>>();
            foreach (var dao in list)
            {
                var bo = CreateNewProduct();
                mapper.MapFromDaoToBo(dao, bo);
            }
            return productList;
        }

        private static IProduct CreateNewProduct()
        {
            return new Library.DomainModel.Products.Product();
        }

        #endregion
    }
}
