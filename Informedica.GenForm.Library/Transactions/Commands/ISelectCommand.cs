using System.Collections.Generic;

namespace Informedica.GenForm.Library.Transactions.Commands
{
    public interface ISelectCommand<T>
    {
        IEnumerable<T> Result { get; }
    }
}