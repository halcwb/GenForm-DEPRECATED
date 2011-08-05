using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Transactions.Commands
{
    public class InsertProductCommand: CommandBase<IProduct, Product>, IInsertCommand<IProduct>
    {

// ReSharper disable UnusedMember.Local
        private InsertProductCommand() {}
// ReSharper restore UnusedMember.Local

        [DefaultConstructor]
        public InsertProductCommand(IProduct product): base(new List<IProduct>{product}) {}

        public IProduct Item
        {
            get { return Items.First(); } 
        }

        #region Implementation of IExecutable

        public override void Execute(GenFormDataContext context)
        {
            GetRepository().Insert(context, Item);
        }

        #endregion
    }
}