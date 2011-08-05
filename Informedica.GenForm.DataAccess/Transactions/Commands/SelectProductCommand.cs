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
    public class SelectProductCommand : ISelectCommand<IProduct>, ICommand, IExecutable
    {
        private readonly Func<Product, Boolean> _selector;
        private IEnumerable<IProduct> _resultList;

        public SelectProductCommand(): this(p => true) {}

        public SelectProductCommand(Func<Product, Boolean> selector)
        {
            _selector = selector;
        }

        public IEnumerable<IProduct> Result
        {
            get { return _resultList; }
        }

        public void Execute(GenFormDataContext context)
        {
            _resultList = GetProductRepository().Fetch(context, _selector);
        }

        private static Repository<IProduct, Product> GetProductRepository()
        {
            return ObjectFactory.GetInstance<Repository<IProduct, Product>>();
        }
    }

    public class SelectProductIntCommand: SelectProductCommand
    {
        public SelectProductIntCommand(Int32 id): base(x => x.ProductId == id) {}
    }

    public class SelectProductStringCommand: SelectProductCommand
    {
        public SelectProductStringCommand(String name): base(x => x.ProductName.Contains(name)) {}
    }
}