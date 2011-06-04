using System;
using System.Collections.Generic;
using System.Data;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.Repositories;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public abstract class Repository<BO, DAO>: IRepository<BO>
    {
        #region Implementation of IRepository<T>

        public abstract IEnumerable<BO> Fetch(int id);
        public abstract IEnumerable<BO> Fetch(string name);
        public abstract void Insert(BO item);

        protected void Insert<TM>(BO item) where TM: IDataMapper<BO, DAO>
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

        protected abstract void InsertOnSubmit(GenFormDataContext ctx, DAO dao);

        protected DAO GetDao()
        {
            return ObjectFactory.GetInstance<DAO>();
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
        public abstract void Delete(BO item);

        #endregion


    }
}
