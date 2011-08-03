using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public class InsertProductCommand: IInsertCommand<IProduct>, ICommand, IExecutable
    {
        private readonly IProduct _product;

// ReSharper disable UnusedMember.Local
        private InsertProductCommand() {}
// ReSharper restore UnusedMember.Local

        [DefaultConstructor]
        public InsertProductCommand(IProduct product)
        {
            _product = product;
        }

        public IProduct Item
        {
            get { return _product; } 
        }

        #region Implementation of IExecutable

        public void Execute(GenFormDataContext context)
        {
            GetProductRepository().Insert(context, _product);
        }

        private static ProductRepository GetProductRepository()
        {
            return ObjectFactory.GetInstance<ProductRepository>();
        }

        #endregion
    }
}