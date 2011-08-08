using System;
using System.Collections.Generic;
using System.Data.Linq;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class Repository<TBo> : IRepository<TBo>
    {
        private GenFormDataContext _context;
        private RollbackObject _rollback;

        /// <summary>
        /// Override of constructor to inject context for testing purposes
        /// </summary>
        /// <param name="context">Datacontext for testing purposes</param>
        public Repository(GenFormDataContext context)
        {
            _context = context;
        }

        [DefaultConstructor]
        public Repository() { }

        #region Insert

        /// <summary>
        /// Insert method that uses local context and transaction handling
        /// </summary>
        /// <param name="bo">Business Object to insert</param>
        public void Insert(TBo bo)
        {
            Insert();
        }

        /// <summary>
        /// Insert method with context and transaction handling managed by caller
        /// </summary>
        /// <param name="context">Data context</param>
        /// <param name="bo">Business Object to insert</param>
        public void Insert(GenFormDataContext context, TBo bo)
        {
        }

        // Locally handled context and transaction
        private void Insert()
        {
            using (var context = GetDataContext())
            {
                SetUpTransaction(context);

                try
                {
                    Submit(context);
                }
                catch (Exception)
                {
                    TearDownTransaction(context);
                    throw;
                }
                EndTransaction(context);
            }
        }


        #endregion

        #region Update

        #endregion

        #region Delete

        /// <summary>
        /// Delete method that uses local context and transaction handling
        /// </summary>
        /// <param name="id">Id of Business Object to be deleted</param>
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Delete method that uses local context and transaction handling
        /// </summary>
        /// <param name="name">Name of business object(s) to be deleted</param>
        public void Delete(String name)
        {
        }

        /// <summary>
        /// Delete method with context handled by caller
        /// </summary>
        /// <param name="context">Data context</param>
        /// <param name="id">Id of Business Object to be deleted</param>
        public void Delete(GenFormDataContext context, Int32 id)
        {
        }

        /// <summary>
        /// Delete method with data context handled by caller
        /// </summary>
        /// <param name="context">Data context</param>
        /// <param name="name">Name of Business Object(s) to be deleted</param>
        public void Delete(GenFormDataContext context, String name)
        {
        }

        /// <summary>
        /// Delete method that uses local context and transaction handling
        /// </summary>
        /// <param name="bo">Business Object to be deleted</param>
        public void Delete(TBo bo)
        {
            Delete(((IIdentifiable<Int32>)bo).Identifier.Id);
        }


        // Delete method that uses a context and a selector to perform delete

        #endregion

        #region Fetch

        /// <summary>
        /// Fetch a Business Object by Id
        /// </summary>
        /// <param name="id">Id of Business Object</param>
        /// <returns>List with one item or empty list</returns>
        public IEnumerable<TBo> Fetch(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fetch Business Object(s) by name 
        /// </summary>
        /// <param name="name">Name of Business Object</param>
        /// <returns>Empty list or with one or more Business Objects</returns>
        public IEnumerable<TBo> Fetch(string name)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Datacontext

        /// <summary>
        /// Get a data context or use the injected datacontext. After the context is returned 
        /// reference to the injected datacontext is destroyed to enable disposal of datacontext
        /// </summary>
        /// <returns>Datacontext</returns>
        public GenFormDataContext GetDataContext()
        {
            if (_context == null) _context = CreateDataContext();

            var context = _context;
            _context = null;
            return context;
        }

        private static GenFormDataContext CreateDataContext()
        {
            return ObjectFactory.With<String>(GetConnectionString()).GetInstance<GenFormDataContext>();
        }

        private static String GetConnectionString()
        {
            return DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm);
        }

        #endregion

        #region Mapping

        #endregion

        #region Transaction Handling

        private void EndTransaction(DataContext context)
        {
            if (_rollback != null) context.Transaction.Rollback();
            else context.Transaction.Commit();
            context.Connection.Close();
        }

        private static void TearDownTransaction(DataContext context)
        {
            context.Transaction.Rollback();
            context.Connection.Close();
        }

        private static void SetUpTransaction(GenFormDataContext context)
        {
            context.Connection.Open();
            context.Transaction = context.Connection.BeginTransaction();
        }

        private static void Submit(GenFormDataContext context)
        {
            context.SubmitChanges();
        }


        #endregion

        #region Helper

        #endregion

        #region RollbackObject


        /// <summary>
        /// Wrapping use of Repository instance in Rollback causes 
        /// insert, update and delete calls on repository to rollback
        /// </summary>
        public IRollbackObject Rollback
        {
            get { return _rollback ?? (_rollback = new RollbackObject(this)); }
        }

        public class RollbackObject : IRollbackObject
        {
            private Repository<TBo> _repository;

            internal RollbackObject(Repository<TBo> repository)
            {
                _repository = repository;
            }

            public void Dispose()
            {
                _repository._rollback = null;
                _repository = null;
            }
        }

        #endregion

    }
}
