using System;
using System.Data;
using System.Web;
using System.Web.Mvc;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess;
using Informedica.GenForm.Services.Environments;
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

        protected ISessionFactory SessionFactory
        {
            get
            {
                return ObjectFactory.GetInstance<ISessionFactory>();
            }
        }

        private static string GetEnvironment()
        {
            var environment = (string)HttpContext.Current.Session["environment"];
            return environment ?? SessionFactoryManager.Test;
        }

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session.IsNewSession) 
                EnvironmentServices.SetHttpSessionCache(filterContext.HttpContext.Session);

            if (GetSessionFactoryFromSessionCache(filterContext) == null) 
                ObjectFactory.Configure(x => x.For<ISessionFactory>().HttpContextScoped().Use(GenFormApplication.GetSessionFactory(GetEnvironment())));
            else 
                ObjectFactory.Configure(x => x.For<ISessionFactory>().HttpContextScoped().Use(GetSessionFactoryFromSessionCache(filterContext)));

            if (GetConnectionFromSessionCache(filterContext) == null)
                _session = SessionFactory.OpenSession();
            else
                _session = SessionFactory.OpenSession(GetConnectionFromSessionCache(filterContext));

            
            //SessionFactoryManager.BuildSchema(GetEnvironment(), _session);
            CurrentSessionContext.Bind(_session);
        }

        private IDbConnection GetConnectionFromSessionCache(ControllerContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
                return (IDbConnection) filterContext.HttpContext.Session["connection"];
            return null;
        }

        private ISessionFactory GetSessionFactoryFromSessionCache(ControllerContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
                return (ISessionFactory)filterContext.HttpContext.Session["sessionfactory"];
            return null;
        }

        public override void OnActionExecuted(
          ActionExecutedContext filterContext)
        {
            try
            {
                var session = CurrentSessionContext.Unbind(SessionFactory);
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
