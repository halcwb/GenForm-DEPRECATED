using System;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions.Commands;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{

    public class DeleteProductCommand: CommandBase<IProduct, Product>, IDeleteCommand<IProduct>
    {
        public DeleteProductCommand(Int32 id): base(p => p.ProductId == id) {}

        public DeleteProductCommand(String name): base(p => p.ProductName == name) {}

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

}