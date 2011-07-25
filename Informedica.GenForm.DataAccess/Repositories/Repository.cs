using System;
using System.Collections.Generic;
using System.Data;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public abstract class Repository<TBo, TDao>: IRepository<TBo>
    {
        #region Implementation of IRepository<T>

        public abstract IEnumerable<TBo> Fetch(int id);
        public abstract IEnumerable<TBo> Fetch(string name);
        public abstract void Insert(TBo item);

        protected void InsertUsingMapper<TM>(TBo item) where TM: IDataMapper<TBo, TDao>
        {
            var mapper = GetMapper<TM>();
            using (var ctx = GetDataContext())
            {
                ctx.Connection.Open();
                try
                {
                    using (var transaction = ctx.Connection.BeginTransaction(IsolationLevel.Unspecified))
                    {
                        ctx.Transaction = transaction;
                        var dao = GetDao();
                        mapper.MapFromBoToDao(item, dao);
                        InsertOnSubmit(ctx, dao);

                        try
                        {
                            ctx.SubmitChanges();
                        }
// ReSharper disable RedundantCatchClause
                        catch (Exception)
                        {
                            throw;
                        }
// ReSharper restore RedundantCatchClause
                        finally
                        {
                            transaction.Rollback();
                        }
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

        protected abstract void InsertOnSubmit(GenFormDataContext ctx, TDao dao);

        protected TDao GetDao()
        {
            return ObjectFactory.GetInstance<TDao>();
        }

        protected static GenFormDataContext GetDataContext()
        {
            var connection = GetConnectionString();
            return ObjectFactory.With<String>(connection).GetInstance<GenFormDataContext>();
        }

        protected static String GetConnectionString()
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
