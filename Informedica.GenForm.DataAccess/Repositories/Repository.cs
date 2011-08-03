using System;
using System.Collections.Generic;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public abstract class Repository<TBo, TDao>: IRepository<TBo>
    {
        private GenFormDataContext _context;

        protected Repository(GenFormDataContext ctx)
        {
            _context = ctx;
        }

        protected Repository() {}

        #region Implementation of IRepository<T>

        public abstract IEnumerable<TBo> Fetch(int id);
        public abstract IEnumerable<TBo> Fetch(string name);
        public abstract void Insert(TBo item);

        protected void InsertUsingMapper<TM>(TBo item) where TM: IDataMapper<TBo, TDao>
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

        protected void InsertUsingContext<TM>(GenFormDataContext context, TBo item) where TM:IDataMapper<TBo,TDao>
        {
            var dao = GetDao();
            GetMapper<TM>().MapFromBoToDao(item, dao);
            InsertOnSubmit(context, dao);

            context.SubmitChanges();
            UpdateBo(item, dao);
        }

        protected abstract void UpdateBo(TBo item, TDao dao);

        protected abstract void InsertOnSubmit(GenFormDataContext ctx, TDao dao);

        protected TDao GetDao()
        {
            return ObjectFactory.GetInstance<TDao>();
        }

        public GenFormDataContext GetDataContext()
        {
            if (_context == null) _context = CreateDataContext();
            
            var context = _context;
            _context = null;
            return context;
        }

        private GenFormDataContext CreateDataContext()
        {
            var connection = GetConnectionString();
            return ObjectFactory.With<String>(connection).GetInstance<GenFormDataContext>();
        }

        protected String GetConnectionString()
        {
            return DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm);
        }

        protected TM GetMapper<TM>()
        {
            return ObjectFactory.GetInstance<TM>();
        }

        public abstract void Delete(int id);
        public abstract void Delete(TBo item);

        #endregion


    }
}
