using Informedica.GenForm.Library.Transactions;

namespace Informedica.GenForm.Library.Factories
{
    public interface ICommandFactory
    {
        ICommand Create<T>();
        ICommand Create<T>(object arg1);
        ICommand Create<T>(object arg1, object arg2);
        ICommand Create<T>(object arg1, object arg2, object arg3);
        ICommand Create<T>(object[] arguments);
    }
}