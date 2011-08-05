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
    public class Repository<TBo, TDao>: IRepository<TBo> 
    {
        private GenFormDataContext _context;

        public Repository(GenFormDataContext context)
        {
            _context = context;
        }

        [DefaultConstructor]
        public Repository() {}

        #region Insert

        public void Insert(TBo item)
        {
            InsertUsingMapper<IDataMapper<TBo, TDao>>(item);
        }

        public void Insert(GenFormDataContext context, TBo item)
        {
            InsertUsingContext<IDataMapper<TBo, TDao>>(context, item);
        }

        private void InsertUsingMapper<TM>(TBo item) where TM: IDataMapper<TBo, TDao>
        {
            using (var ctx = GetDataContext())
            {
                try
                {
                        ctx.Connection.Open();
                        ctx.Transaction = ctx.Connection.BeginTransaction();
                        var dao = GetDao();
                        GetMapper<TM>().MapFromBoToDao(item, dao);
                        InsertOnSubmit(ctx, dao);
                    try
                    {
                        ctx.SubmitChanges();
                        UpdateBo(item, dao);
                    }
// ReSharper disable RedundantCatchClause
                        catch (Exception)
                        {
                            throw;
                        }
// ReSharper restore RedundantCatchClause
                        finally
                        {
                            ctx.Transaction.Rollback();
                        }

                }
// ReSharper disable RedundantCatchClause
                catch (Exception)
                {
                    throw;
                }
// ReSharper restore RedundantCatchClause
                finally
                {
                    ctx.Connection.Close();
                }
            }
        }

        private static void UpdateBo(TBo item, TDao dao)
        {
            var update = ObjectFactory.GetInstance<UpdateBo<TBo, TDao>>();
            update(item, dao);
        }

        private static void InsertOnSubmit(GenFormDataContext ctx, TDao dao)
        {
            var insert = ObjectFactory.GetInstance<InsertOnSubmit<TDao>>();
            insert(ctx, dao);
        }

        private void InsertUsingContext<TM>(GenFormDataContext context, TBo item) where TM : IDataMapper<TBo, TDao>
        {
            var dao = GetDao();
            GetMapper<TM>().MapFromBoToDao(item, dao);
            InsertOnSubmit(context, dao);

            context.SubmitChanges();
            UpdateBo(item, dao);
        }

        #endregion

        #region Update

        #endregion

        #region Delete

        public void Delete(int id)
        {
            using (var ctx = GetDataContext())
            {
                var selector = GetIdSelector(id);
                Delete(ctx, selector);
            }
        }

        public void Delete(String name)
        {
            var selector = GetNameSelector(name);
            Delete(GetDataContext(), selector);
        }

        public void Delete(GenFormDataContext context, Int32 id)
        {
            var selector = GetIdSelector(id);
            Delete(context, selector);
        }

        public void Delete(TBo item)
        {
            Delete(((IIdentifiable<Int32>)item).Identifier.Id);
        }

        public void Delete(GenFormDataContext context, Func<TDao, Boolean> selector)
        {
            var delete = GetDeleteMethod();
            delete(context, selector);
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
            var selector = GetIdSelector(id);
            return Fetch(GetDataContext(), selector);
        }

        public IEnumerable<TBo> Fetch(string name)
        {
            var selector = GetNameSelector(name);
            return Fetch(GetDataContext(), selector);
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

        private static Func<TDao, bool> GetIdSelector(int id)
        {
            return ObjectFactory.GetInstance<CreateIdSelector<TDao>>()(id);
        }

        private static TBo CreateNewBo()
        {
            return ObjectFactory.GetInstance<TBo>();
        }

        private static Func<TDao, bool> GetNameSelector(string name)
        {
            return ObjectFactory.GetInstance<CreateNameSelector<TDao>>()(name);
        }

        #endregion

    }
}
