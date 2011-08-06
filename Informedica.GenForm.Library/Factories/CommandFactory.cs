using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;

namespace Informedica.GenForm.Library.Factories
{
    public class CommandFactory
    {
        public static ICommand CreateInsertCommand<T>(T item)
        {
            return (ICommand)Factory.ObjectFactory.Instance.GetInstanceWith<IInsertCommand<T>, T>(item);
        }

        public static ICommand CreateSelectCommand<T>()
        {
            return (ICommand)Factory.ObjectFactory.Instance.Create<ISelectCommand<T>>();
        }

        public static ICommand CreateDeleteCommand<T>(object arg1)
        {
            return (ICommand)Factory.ObjectFactory.Instance.Create<IDeleteCommand<T>>(arg1);
        }

        public static ICommand CreateSelectCommand<T>(object arg1)
        {
            return (ICommand)Factory.ObjectFactory.Instance.Create<ISelectCommand<T>>(arg1);
        }
    }

}
