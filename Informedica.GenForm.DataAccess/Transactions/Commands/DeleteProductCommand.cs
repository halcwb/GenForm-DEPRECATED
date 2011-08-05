using System;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{

    public class DeleteProductCommand: CommandBase<IProduct, Product>, IDeleteCommand<IProduct>, ICommand, IExecutable
    {
        public DeleteProductCommand(Func<Product, Boolean> selector): base(selector) {}

        #region Implementation of IDeleteCommand<IProduct>

        #endregion

        #region Implementation of IExecutable

        #endregion

        public override void Execute(GenFormDataContext context)
        {
            GetRepository().Delete(context, Selector);
        }
    }

    public class DeleteProductIntCommand : DeleteProductCommand
    {
        public DeleteProductIntCommand(Int32 id): base(x => x.ProductId == id) {}
    }

    public class DeleteProductStringCommand: DeleteProductCommand
    {
        public DeleteProductStringCommand(String name): base(x => x.ProductName == name) {}
    }
}