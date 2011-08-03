using System;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Transactions;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Transactions
{
    public class TransactionManager: ITransactionManager
    {
        private readonly GenFormDataContext _context;
        private CommandList _commandList;


        public TransactionManager(GenFormDataContext context, CommandList commandList)
        {
            _context = context;
            _commandList = commandList;
        }

        [DefaultConstructor]
        public TransactionManager(CommandList commandList): this(GetDataContext(), commandList)
        {}

        private static GenFormDataContext GetDataContext()
        {
            return ObjectFactory.With<String>(DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm)).GetInstance<GenFormDataContext>();
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion

        public void StartTransaction()
        {
            _context.Connection.Open();
            _context.Transaction = _context.Connection.BeginTransaction();
        }

        public void RollBackTransaction()
        {
            _context.Transaction.Rollback();
        }

        public void CommitTransaction()
        {
            _context.Transaction.Commit();
        }

        public void ExecuteCommands()
        {
            foreach (var command in _commandList.Commands)
            {
                try
                {
                    ((IExecutable)command).Execute(_context);

                }
                catch (Exception)
                {
                    RollBackTransaction();
                    throw;
                }
            }
        }
    }
}