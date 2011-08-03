using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public class SelectProductCommand: ISelectCommand<IProduct>, ICommand, IExecutable
    {
        private Func<Product, Boolean> _selector;

        public SelectProductCommand(Int32 id): this(x => x.ProductId == id) {}

        public SelectProductCommand(String name): this(x => x.ProductName.StartsWith(name)) {}

        public SelectProductCommand(Func<Product, Boolean> selector)
        {
             _selector = selector;
        }

        #region Implementation of ISelectCommand<IProduct>

        public IProduct Item
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Implementation of IExecutable

        public void Execute(GenFormDataContext context)
        {
            GetProductRepository().Fetch(context, _selector);
        }

        private ProductRepository GetProductRepository()
        {
            return ObjectFactory.GetInstance<ProductRepository>();
        }

        #endregion
    }
}