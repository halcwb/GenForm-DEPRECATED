using System;
using System.Collections.Generic;
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

        public Repository(GenFormDataContext context)
        {
            _context = context;
        }

        [DefaultConstructor]
        public Repository() { }

        #region Insert

        public void Insert(TBo item)
        {
            Insert<IDataMapper<TBo, TDao>>(item);
        }

        public void Insert(GenFormDataContext context, TBo item)
        {
            Insert<IDataMapper<TBo, TDao>>(context, item);
        }

        private void Insert<TM>(TBo item) where TM : IDataMapper<TBo, TDao>
        {
            using (var context = GetDataContext())
            {
                try
                {
                    context.Connection.Open();
                    context.Transaction = context.Connection.BeginTransaction();
                    var dao = GetDao();
                    GetMapper<TM>().MapFromBoToDao(item, dao);
                    InsertOnSubmit(context, dao);
                    try
                    {
                        context.SubmitChanges();
                        RefreshBo(item, dao);
                    }
                    catch (Exception)
                    {
                        _rollback = new RollbackObject(this);
                        throw;
                    }
                    finally
                    {
                        if (_rollback != null) context.Transaction.Rollback();
                        else
                        {
                            context.Transaction.Commit();
                        }
                    }

                }
                finally
                {
                    context.Connection.Close();
                }
            }
        }

        private static void RefreshBo(TBo bo, TDao dao)
        {
            GetRefreshMethod()(bo, dao);
        }

        private static Refresh<TBo, TDao> GetRefreshMethod()
        {
            return ObjectFactory.GetInstance<Refresh<TBo, TDao>>();
        }

        private static void InsertOnSubmit(GenFormDataContext ctx, TDao dao)
        {
            GetInsertMethod()(ctx, dao);
        }

        private static Insert<TDao> GetInsertMethod()
        {
            return ObjectFactory.GetInstance<Insert<TDao>>();
        }

        private void Insert<TM>(GenFormDataContext context, TBo item) where TM : IDataMapper<TBo, TDao>
        {
            var dao = GetDao();
            GetMapper<TM>().MapFromBoToDao(item, dao);
            InsertOnSubmit(context, dao);

            context.SubmitChanges();
            RefreshBo(item, dao);
        }

        #endregion

        #region Update

        #endregion

        #region Delete

        public void Delete(int id)
        {
            using (var context = GetDataContext())
            {
                Delete(context, GetIdSelector(id));
            }
        }

        public void Delete(String name)
        {
            Delete(GetDataContext(), GetNameSelector(name));
        }

        public void Delete(GenFormDataContext context, Int32 id)
        {
            Delete(context, GetIdSelector(id));
        }

        public void Delete(TBo item)
        {
            Delete(((IIdentifiable<Int32>)item).Identifier.Id);
        }

        public void Delete(GenFormDataContext context, Func<TDao, Boolean> selector)
        {
            GetDeleteMethod()(context, selector);
            context.SubmitChanges();
        }

        private static Delete<TDao> GetDeleteMethod()
        {
            return ObjectFactory.GetInstance<Delete<TDao>>();
        }

        #endregion

        #region Fetch

        public IEnumerable<TBo> Fetch(GenFormDataContext context, Func<TDao, Boolean> selector)
        {
            var fetch = ObjectFactory.GetInstance<Fetch<TDao>>();
            var list = fetch(context, selector);
            return CreateBoListFromDaoList(list);
        }

        public IEnumerable<TBo> Fetch(int id)
        {
            return Fetch(GetDataContext(), GetIdSelector(id));
        }

        public IEnumerable<TBo> Fetch(string name)
        {
            return Fetch(GetDataContext(), GetNameSelector(name));
        }

        private IEnumerable<TBo> CreateBoListFromDaoList(IEnumerable<TDao> list)
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

        public GenFormDataContext GetDataContext()
        {
            if (_context == null) _context = CreateDataContext();

            var context = _context;
            _context = null;
            return context;
        }

        private GenFormDataContext CreateDataContext()
        {
            return ObjectFactory.With<String>(GetConnectionString()).GetInstance<GenFormDataContext>();
        }

        protected String GetConnectionString()
        {
            return DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm);
        }

        #endregion

        #region Mapping

        protected TM GetMapper<TM>()
        {
            return ObjectFactory.GetInstance<TM>();
        }

        protected TDao GetDao()
        {
            return ObjectFactory.GetInstance<TDao>();
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

        public IRollbackObject Rollback
        {
            get { return _rollback ?? (_rollback = new RollbackObject(this)); }
        }

        public class RollbackObject : IRollbackObject
        {
            private Repository<TBo, TDao> _repository;

            public RollbackObject(Repository<TBo, TDao> repository)
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
