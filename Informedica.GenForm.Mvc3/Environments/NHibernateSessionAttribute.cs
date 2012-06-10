using System;
using System.Web;
using System.Web.Mvc;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Mvc3.Controllers;
using Informedica.GenForm.Services;
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

        public NHibernateSessionAttribute()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null) return;
            
            var cache = new HttpSessionCache(new HttpSessionStateWrapper(HttpContext.Current.Session));
            ObjectFactory.Configure(x => x.For<ISessionCache>().Use(cache));
        }

        public IDatabaseServices DatabaseServices { get; set; }

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
            var session = CurrentSessionContext.Unbind(TryGetSessionFactory(filterContext));
            session.Close();
        }

        private static ISessionFactory TryGetSessionFactory(ControllerContext context)
        {
            try
            {
                return SessionStateManager.SessionFactory;

            }
            catch (Exception e)
            {
                throw new Exception(context.Controller + ": " + e);
            }
        }
    }
}
