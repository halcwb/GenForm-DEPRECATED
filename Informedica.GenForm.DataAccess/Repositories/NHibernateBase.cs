using System;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public abstract class NHibernateBase
    {
        protected readonly ISessionFactory Factory;

        protected NHibernateBase(ISessionFactory factory)
        {
            Factory = factory;
        }

        protected virtual ISession Session
        {
            get
            {
                return Factory.GetCurrentSession();
            }
        }

        protected virtual TResult Transact<TResult>(Func<TResult> func)
        {
            // wrap
            if (!Session.Transaction.IsActive)
            {
                TResult result;
                using (var transaction = Session.BeginTransaction())
                {
                    result = func.Invoke();
                    transaction.Commit();
                }
                return result;
            }
            // Already wrapped
            return func.Invoke();
        }

        protected virtual void Transact(Action action)
        {
            Transact(() =>
                {
                    action.Invoke();
                    return false;
                });
        }
    }
}
