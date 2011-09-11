using System;
using System.Web.Mvc;
using Informedica.GenForm.Assembler;
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
                return MvcApplication.SessionFactory;
            }
        }

        public override void OnActionExecuting(
          ActionExecutingContext filterContext)
        {
            string environment = "GenFormTest";
            ObjectFactory.Configure(x => x.For<ISessionFactory>().HttpContextScoped().Use(GenFormApplication.GetSessionFactory(environment)));
            var session = SessionFactory.OpenSession();
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
