using System;
using System.Web.Mvc;
using Informedica.GenForm.Mvc3.Controllers;
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
                return SessionStateManager.SessionFactory.GetCurrentSession();
            }
        }

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            if (filterContext.Controller is LoginController) return;

            base.OnActionExecuting(filterContext);
            Session.BeginTransaction();
        }

        public override void OnResultExecuted(
          ResultExecutedContext filterContext)
        {
            if (filterContext.Controller is LoginController)
            {
                base.OnResultExecuted(filterContext);
                return;
            }

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
            } finally
            {
                base.OnResultExecuted(filterContext);                
            }
        }

    }
}
