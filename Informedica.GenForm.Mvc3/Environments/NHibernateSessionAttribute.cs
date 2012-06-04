using System;
using System.Web;
using System.Web.Mvc;
using Informedica.GenForm.Mvc3.Controllers;
using NHibernate;
using NHibernate.Context;
using StructureMap;

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
            var session = filterContext.HttpContext.Session;

            SessionStateManager.UseSessionFactoryFromApplicationOrSessionState(session);
            OpenSession(session);
            BindSessionToCurrentSessionContext();
        }

        private void OpenSession(HttpSessionStateBase session)
        {
            _session = SessionStateManager.OpenSession(session);
        }

        private void BindSessionToCurrentSessionContext()
        {
//SessionFactoryManager.BuildSchema(GetEnvironment(), _session);
            CurrentSessionContext.Bind(_session);
        }

        public override void OnActionExecuted(
          ActionExecutedContext filterContext)
        {
            try
            {
                var session = CurrentSessionContext.Unbind(SessionStateManager.SessionFactory);
                session.Close();

            }
// ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
// ReSharper restore EmptyGeneralCatchClause
            {
                // ToDo: dirty hack, have to fix this
            }
        }

    }
}
