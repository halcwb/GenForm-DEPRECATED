using Informedica.GenForm.DataAccess.Transactions;
using Informedica.GenForm.DataAccess.Transactions.Commands;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler.Assemblers
{
    public class TransactionAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<ITransactionManager>().Use<TransactionManager>();

            _registry.For<IInsertCommand<IProduct>>().Use<InsertProductCommand>();
            _registry.For<ISelectCommand<IProduct>>().Use<SelectProductCommand>();
            _registry.For<IDeleteCommand<IProduct>>().Use<DeleteProductCommand>();

            _registry.For<IInsertCommand<ISubstance>>().Use<InsertSubstanceCommand>();
            _registry.For<ISelectCommand<ISubstance>>().Use<SelectSubstanceCommand>();


            return _registry;
        }
    }

}
