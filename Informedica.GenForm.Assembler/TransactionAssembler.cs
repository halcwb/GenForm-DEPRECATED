using Informedica.GenForm.DataAccess.Transactions;
using Informedica.GenForm.DataAccess.Transactions.Commands;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using StructureMap.Configuration.DSL;

namespace Informedica.GenForm.Assembler
{
    public class TransactionAssembler
    {
        private static Registry _registry;

        public static Registry RegisterDependencies()
        {
            _registry = new Registry();

            _registry.For<ITransactionManager>().Use<TransactionManager>();

            _registry.For<IInsertCommand<IProduct>>().Use<InsertProductCommand>();
            _registry.For<IStringSelectCommand<IProduct>>().Use<SelectProductStringCommand>();
            _registry.For<IIntSelectCommand<IProduct>>().Use<SelectProductIntCommand>();
            _registry.For<ISelectCommand<IProduct>>().Use<SelectProductCommand>();
            _registry.SelectConstructor(() => new SelectProductCommand());
            _registry.For<IStringDeleteCommand<IProduct>>().Use<DeleteProductStringCommand>();
            _registry.For<IIntDeleteCommand<IProduct>>().Use<DeleteProductIntCommand>();

            _registry.For<IInsertCommand<ISubstance>>().Use<InsertSubstanceCommand>();
            _registry.For<IStringSelectCommand<ISubstance>>().Use<SelectSubstanceStringCommand>();
            _registry.For<IIntSelectCommand<ISubstance>>().Use<SelectSubstanceIntCommand>();
            _registry.For<ISelectCommand<ISubstance>>().Use<SelectSubstanceCommand>();
            _registry.SelectConstructor(() => new SelectSubstanceCommand());
            _registry.For<IStringDeleteCommand<ISubstance>>().Use<DeleteSubstanceStringCommand>();
            _registry.For<IIntDeleteCommand<ISubstance>>().Use<DeleteSubstanceIntCommand>();


            return _registry;
        }
    }

}
