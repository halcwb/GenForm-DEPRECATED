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
        private string _controller;

        protected ISession Session
        {
            get
            {
                try
                {
                    return SessionStateManager.SessionFactory.GetCurrentSession();
                }
                catch (Exception e)
                {
                    throw new Exception(_controller + ": " + e);
                }
            }
        }

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            if (DoNotRunAttributeForSpecificControllers(filterContext)) return;
            _controller = filterContext.Controller.ToString();

            base.OnActionExecuting(filterContext);
            Session.BeginTransaction();
        }

        private static bool DoNotRunAttributeForSpecificControllers(ControllerContext filterContext)
        {
            return filterContext.Controller is LoginController || filterContext.Controller is HomeController;
        }

        public override void OnActionExecuted(
            ActionExecutedContext filterContext)
        {
            if (DoNotRunAttributeForSpecificControllers(filterContext)) return;

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
            finally
            {
                base.OnActionExecuted(filterContext);
            }
        }

    }
}
