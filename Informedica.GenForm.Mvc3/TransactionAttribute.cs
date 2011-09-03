using System;
using System.Web.Mvc;
using NHibernate;

namespace Informedica.GenForm.Mvc3
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
            var tx = Session.Transaction;
            if (tx != null && tx.IsActive)
                Session.Transaction.Commit();

            base.OnResultExecuted(filterContext);
        }

    }
}
