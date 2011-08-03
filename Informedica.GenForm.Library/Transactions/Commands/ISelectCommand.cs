using System.Collections.Generic;

namespace Informedica.GenForm.Library.Transactions.Commands
{
    public interface ISelectCommand<T>: IStringSelectCommand<T>, IIntSelectCommand<T>
    {
        IEnumerable<T> Result { get; }
    }

    public interface IIntSelectCommand<T>
    {
    }

    public interface IStringSelectCommand<T>
    {
    }
}