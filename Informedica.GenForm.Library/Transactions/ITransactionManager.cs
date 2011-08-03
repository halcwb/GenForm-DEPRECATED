using System;

namespace Informedica.GenForm.Library.Transactions
{
    public interface ITransactionManager : IDisposable
    {
        void StartTransaction();
        void RollBackTransaction();
        void CommitTransaction();
        void ExecuteCommands();
    }
}