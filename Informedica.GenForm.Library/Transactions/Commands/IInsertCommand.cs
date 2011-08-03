namespace Informedica.GenForm.Library.Transactions.Commands
{
    public interface IInsertCommand<T>
    {
        T Item { get; }
    }
}