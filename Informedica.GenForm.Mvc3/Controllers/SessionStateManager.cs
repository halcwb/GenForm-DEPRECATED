using System.Data;
using System.Web;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess;
using NHibernate;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public static class SessionStateManager
    {
        public static void UseSessionFactoryFromApplicationOrSessionState(HttpSessionStateBase session)
        {
            if (GetSessionFactoryFromSessionState(session) == null)
                ObjectFactory.Configure(
                    x =>
                    x.For<ISessionFactory>().Use(GenFormApplication.GetSessionFactory(GetEnvironment(session))));
            else
            {
                ObjectFactory.Configure(
                    x =>
                    x.For<ISessionFactory>().Use(GetSessionFactoryFromSessionState(session)));
            }
        }

        public static ISessionFactory GetSessionFactoryFromSessionState(HttpSessionStateBase session)
        {
            if (session != null)
                return (ISessionFactory)session["sessionfactory"];
            return null;
        }

        public static string GetEnvironment(HttpSessionStateBase session)
        {
            if (session == null)
            {
                return SessionFactoryManager.Test;
            }
            var environment = (string)session["environment"];
            return environment ?? SessionFactoryManager.Test;
        }

        public static IDbConnection GetConnectionFromSessionState(HttpSessionStateBase session)
        {
            if (session != null)
                return (IDbConnection)session["connection"];
            return null;
        }
    }
}