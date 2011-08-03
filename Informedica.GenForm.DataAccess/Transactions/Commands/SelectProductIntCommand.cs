using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public class SelectProductCommandBase : ISelectCommand<IProduct>, ICommand, IExecutable
    {
        private Func<Product, Boolean> _selector;
        private IEnumerable<IProduct> _resultList;

        public SelectProductCommandBase(Func<Product, Boolean> selector)
        {
            _selector = selector;
            _resultList = new List<IProduct>();
        }

        public IEnumerable<IProduct> Result
        {
            get { return _resultList; }
        }

        public void Execute(GenFormDataContext context)
        {
            _resultList = GetProductRepository().Fetch(context, _selector);
        }

        private ProductRepository GetProductRepository()
        {
            return ObjectFactory.GetInstance<ProductRepository>();
        }
    }

    public class SelectProductIntCommand: SelectProductCommandBase
    {
        public SelectProductIntCommand(Int32 id): base(x => x.ProductId == id) {}

    }

    public class SelectProductStringCommand: SelectProductCommandBase
    {
        public SelectProductStringCommand(String name): base(x => x.ProductName.Contains(name)) {}
    }
}