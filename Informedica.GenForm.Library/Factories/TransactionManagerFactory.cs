using Informedica.GenForm.Library.Transactions;
using StructureMap;

namespace Informedica.GenForm.Library.Factories
{
    public class TransactionManagerFactory
    {
        public static ITransactionManager CreateTransactionManager(CommandQueue commandQueue)
        {
            return ObjectFactory.With(commandQueue).GetInstance<ITransactionManager>();
        }

    }
}
