using System;
using System.Collections.Generic;
using System.Data.Linq;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Repositories.Delegates;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public class Repository<TBo, TDao> : IRepository<TBo>
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
            Insert<IDataMapper<TBo, TDao>>(bo);
        }

        /// <summary>
        /// Insert method with context and transaction handling managed by caller
        /// </summary>
        /// <param name="context">Data context</param>
        /// <param name="bo">Business Object to insert</param>
        public void Insert(GenFormDataContext context, TBo bo)
        {
            Insert<IDataMapper<TBo, TDao>>(context, bo);
        }

        // Locally handled context and transaction
        private void Insert<TM>(TBo bo) where TM : IDataMapper<TBo, TDao>
        {
            using (var context = GetDataContext())
            {
                SetUpTransaction(context);

                var dao = InsertMappedDao<TM>(context, bo);
                try
                {
                    SubmitAndRefresh(context, bo, dao);
                }
                catch (Exception)
                {
                    TearDownTransaction(context);
                    throw;
                }
                EndTransaction(context);
            }
        }

        // Insert with context managed by caller
        private static TDao InsertMappedDao<TM>(GenFormDataContext context, TBo bo) where TM : IDataMapper<TBo, TDao>
        {
            var dao = CreateDao();
            GetMapper<TM>().MapFromBoToDao(bo, dao);

            InsertOnSubmit(context, dao);
            return dao;
        }

        // Refresh bo after insert with for example new id
        private static void RefreshBo(TBo bo, TDao dao)
        {
            GetRefreshMethod()(bo, dao);
        }

        // Refresh method to be used to refresh bo
        private static Refresh<TBo, TDao> GetRefreshMethod()
        {
            return ObjectFactory.GetInstance<Refresh<TBo, TDao>>();
        }

        // Perform the actual insert
        private static void InsertOnSubmit(GenFormDataContext ctx, TDao dao)
        {
            GetInsertMethod()(ctx, dao);
        }

        // Get the method to insert the bo
        private static Insert<TDao> GetInsertMethod()
        {
            return ObjectFactory.GetInstance<Insert<TDao>>();
        }

        // Insert using the context managed by caller
        private static void Insert<TM>(GenFormDataContext context, TBo bo) where TM : IDataMapper<TBo, TDao>
        {
            var dao = InsertMappedDao<TM>(context, bo);

            SubmitAndRefresh(context, bo, dao);
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
                Delete(GetIdSelector(id));
        }

        /// <summary>
        /// Delete method that uses local context and transaction handling
        /// </summary>
        /// <param name="name">Name of business object(s) to be deleted</param>
        public void Delete(String name)
        {
            Delete(GetNameSelector(name));
        }

        /// <summary>
        /// Delete method with context handled by caller
        /// </summary>
        /// <param name="context">Data context</param>
        /// <param name="id">Id of Business Object to be deleted</param>
        public void Delete(GenFormDataContext context, Int32 id)
        {
            Delete(context, GetIdSelector(id));
        }

        /// <summary>
        /// Delete method with data context handled by caller
        /// </summary>
        /// <param name="context">Data context</param>
        /// <param name="name">Name of Business Object(s) to be deleted</param>
        public void Delete(GenFormDataContext context, String name)
        {
            Delete(context, GetNameSelector(name));
        }

        /// <summary>
        /// Delete method that uses local context and transaction handling
        /// </summary>
        /// <param name="bo">Business Object to be deleted</param>
        public void Delete(TBo bo)
        {
            Delete(((IIdentifiable<Int32>)bo).Identifier.Id);
        }

        // Localy managed delete method
        private void Delete(Func<TDao, Boolean> selector)
        {
            using (var context = GetDataContext())
            {
                SetUpTransaction(context);
                GetDeleteMethod()(context, selector);
                try
                {
                    context.SubmitChanges();
                }
                catch (Exception)
                {
                    TearDownTransaction(context);
                    throw;
                }
                EndTransaction(context);
            }
        }

        /// <summary>
        /// Delete method with context managed by caller
        /// </summary>
        /// <param name="context">Datacontext</param>
        /// <param name="selector">Selector method to identify object(s) to delete</param>
        public void Delete(GenFormDataContext context, Func<TDao, Boolean> selector)
        {
            GetDeleteMethod()(context, selector);
            context.SubmitChanges();
        }

        // Delete method that uses a context and a selector to perform delete
        private static Delete<TDao> GetDeleteMethod()
        {
            return ObjectFactory.GetInstance<Delete<TDao>>();
        }

        #endregion

        #region Fetch

        /// <summary>
        /// Fetch Business Objects according to a selector function with datacontext
        /// managed by caller
        /// </summary>
        /// <param name="context">Datacontext</param>
        /// <param name="selector">Selector function</param>
        /// <returns>List of fetched Business Objects</returns>
        public IEnumerable<TBo> Fetch(GenFormDataContext context, Func<TDao, Boolean> selector)
        {
            var fetch = ObjectFactory.GetInstance<Fetch<TDao>>();
            var list = fetch(context, selector);
            return CreateBoListFromDaoList(list);
        }

        /// <summary>
        /// Fetch a Business Object by Id
        /// </summary>
        /// <param name="id">Id of Business Object</param>
        /// <returns>List with one item or empty list</returns>
        public IEnumerable<TBo> Fetch(int id)
        {
            return Fetch(GetDataContext(), GetIdSelector(id));
        }

        /// <summary>
        /// Fetch Business Object(s) by name 
        /// </summary>
        /// <param name="name">Name of Business Object</param>
        /// <returns>Empty list or with one or more Business Objects</returns>
        public IEnumerable<TBo> Fetch(string name)
        {
            return Fetch(GetDataContext(), GetNameSelector(name));
        }

        private static IEnumerable<TBo> CreateBoListFromDaoList(IEnumerable<TDao> list)
        {
            IList<TBo> boList = new List<TBo>();
            var mapper = GetMapper<IDataMapper<TBo, TDao>>();
            foreach (var dao in list)
            {
                var bo = CreateNewBo();
                mapper.MapFromDaoToBo(dao, bo);
                boList.Add(bo);
            }
            return boList;
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

        private static TM GetMapper<TM>()
        {
            return ObjectFactory.GetInstance<TM>();
        }

        private static TDao CreateDao()
        {
            return ObjectFactory.GetInstance<TDao>();
        }

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

        private static void SubmitAndRefresh(GenFormDataContext context, TBo bo, TDao dao)
        {
            context.SubmitChanges();
            RefreshBo(bo, dao);
        }


        #endregion

        #region Helper

        private static Func<TDao, Boolean> GetIdSelector(Int32 id)
        {
            return ObjectFactory.GetInstance<SelectorOfInt<TDao>>()(id);
        }

        private static TBo CreateNewBo()
        {
            return ObjectFactory.GetInstance<TBo>();
        }

        private static Func<TDao, Boolean> GetNameSelector(String name)
        {
            return ObjectFactory.GetInstance<SelectorOfString<TDao>>()(name);
        }

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
            private Repository<TBo, TDao> _repository;

            internal RollbackObject(Repository<TBo, TDao> repository)
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
