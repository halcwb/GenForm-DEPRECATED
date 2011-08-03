namespace Informedica.GenForm.Library.Transactions.Commands
{
    public interface ISelectCommand<T>
    {
        T Item { get; }
    }
}