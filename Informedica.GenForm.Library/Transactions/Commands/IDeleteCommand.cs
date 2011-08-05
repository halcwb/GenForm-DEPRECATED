namespace Informedica.GenForm.Library.Transactions.Commands
{
    public interface IDeleteCommand<T>: IIntDeleteCommand<T>, IStringDeleteCommand<T>
    {
    }

    public interface IIntDeleteCommand<T>
    {
    }

    public interface IStringDeleteCommand<T>
    {
    }
}