using System;
using System.Web.Mvc;
using NHibernate;

namespace Informedica.GenForm.Mvc3.Environments
{

    [AttributeUsage(AttributeTargets.Method,
      AllowMultiple = true)]
    public class TransactionAttribute
      : NHibernateSessionAttribute
    {

        protected ISession Session
        {
            get
            {
                return SessionFactory.GetCurrentSession();
            }
        }

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Session.BeginTransaction();
        }

        public override void OnResultExecuted(
          ResultExecutedContext filterContext)
        {
            try
            {
                var tx = Session.Transaction;
                if (tx != null && tx.IsActive)
                    Session.Transaction.Commit();

            }
// ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
// ReSharper restore EmptyGeneralCatchClause
            {
                //ToDo: dirty hack, do nothing have to fix this 
            }
            base.OnResultExecuted(filterContext);
        }

    }
}
