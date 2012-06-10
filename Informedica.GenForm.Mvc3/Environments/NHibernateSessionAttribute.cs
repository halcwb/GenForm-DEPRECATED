using System;
using System.Web;
using System.Web.Mvc;
using Informedica.GenForm.Mvc3.Controllers;
using NHibernate;
using NHibernate.Context;

namespace Informedica.GenForm.Mvc3.Environments
{
    [AttributeUsage(AttributeTargets.Method,
      AllowMultiple = false)]
    public class NHibernateSessionAttribute
      : ActionFilterAttribute
    {
        private ISession _session;

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            var sessionState = filterContext.HttpContext.Session;

            SessionStateManager.UseSessionFactoryFromApplicationOrSessionState(sessionState);
            OpenSession(sessionState);
            BindSessionToCurrentSessionContext();
        }

        private void OpenSession(HttpSessionStateBase sessionState)
        {
            _session = SessionStateManager.OpenSession(sessionState);
        }

        private void BindSessionToCurrentSessionContext()
        {
//SessionFactoryManager.BuildSchema(GetEnvironment(), _session);
            CurrentSessionContext.Bind(_session);
        }

        public override void OnActionExecuted(
          ActionExecutedContext filterContext)
        {
            var session = CurrentSessionContext.Unbind(SessionStateManager.SessionFactory);
            session.Close();
        }

    }
}
