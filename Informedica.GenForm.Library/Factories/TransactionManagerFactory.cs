using Informedica.GenForm.Library.Transactions;

namespace Informedica.GenForm.Library.Factories
{
    public class TransactionManagerFactory
    {
        public static ITransactionManager CreateTransactionManager(CommandQueue commandQueue)
        {
            return Factory.ObjectFactory.Instance.With(commandQueue).GetInstance<ITransactionManager>();
        }

    }
}
