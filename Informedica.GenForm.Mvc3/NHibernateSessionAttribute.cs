using System;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Context;

namespace Informedica.GenForm.Mvc3
{
    [AttributeUsage(AttributeTargets.Method,
      AllowMultiple = false)]
    public class NHibernateSessionAttribute
      : ActionFilterAttribute
    {

        protected ISessionFactory SessionFactory
        {
            get
            {
                return MvcApplication.SessionFactory;
            }
        }

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        public override void OnActionExecuted(
          ActionExecutedContext filterContext)
        {
            var session = CurrentSessionContext.Unbind(SessionFactory);
            session.Close();
        }

    }
}
