using System;
using System.Web;
using System.Web.Mvc;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess;
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

        protected ISessionFactory SessionFactory
        {
            get
            {
                return MvcApplication.GetSessionFactory(GetEnvironment());
            }
        }

        private static string GetEnvironment()
        {
            var environment = (string)HttpContext.Current.Session["environment"];
            return environment ?? "TestGenForm";
        }

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            ObjectFactory.Configure(x => x.For<ISessionFactory>().HttpContextScoped().Use(GenFormApplication.GetSessionFactory(GetEnvironment())));
            var session = SessionFactory.OpenSession();
            SessionFactoryManager.BuildSchema(GetEnvironment(), session);
            CurrentSessionContext.Bind(session);
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
