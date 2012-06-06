using System.Data;
using System.Web;
using Informedica.DataAccess.Configurations;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess;
using Informedica.GenForm.Library.Services.Users;
using NHibernate;
using NHibernate.Context;
using StructureMap;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public static class SessionStateManager
    {
        public const string SessionFactorySetting = "sessionfactory";
        public const string EnvironmentSetting = "environment";
        public const string ConnectionSetting = "connection";

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
                return (ISessionFactory)session[SessionFactorySetting];
            return null;
        }

        public static string GetEnvironment(HttpSessionStateBase session)
        {
            if (session == null)
            {
                return SessionFactoryManager.Test;
            }
            var environment = (string)session[EnvironmentSetting];
            return environment ?? SessionFactoryManager.Test;
        }

        public static IDbConnection GetConnectionFromSessionState(HttpSessionStateBase session)
        {
            if (session != null)
                return (IDbConnection)session[ConnectionSetting];
            return null;
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                return ObjectFactory.GetInstance<ISessionFactory>();
            }
        }

        public static ISession OpenSession(HttpSessionStateBase session)
        {
            return GetConnectionFromSessionState(session) == null ? 
                        SessionFactory.OpenSession() : 
                        SessionFactory.OpenSession(GetConnectionFromSessionState(session));
        }

        public static void SetupDatabase(HttpSessionStateBase sessionState)
        {
            // Will cache the connection if in memory database
            SetupConfiguration(sessionState);

            // If database config is in memory, connection will be cached in in session state
            var conn = GetConnectionFromSessionState(sessionState);
            if (conn == null) return;

            // Connection has been cache so in memory database
            SetupInMemoryDatabase(sessionState, conn);
        }

        private static void SetupConfiguration(HttpSessionStateBase sessionState)
        {
            var environment = GetEnvironment(sessionState);
            var envConf = ConfigurationManager.Instance.GetConfiguration(environment);
            envConf.GetConnection();
        }

        private static void SetupInMemoryDatabase(HttpSessionStateBase sessionState, IDbConnection conn)
        {
            var fact = SessionFactoryManager.GetSessionFactory(GetEnvironment(sessionState));
            sessionState["sessionfactory"] = fact;

            UseSessionFactoryFromApplicationOrSessionState(sessionState);
            var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession(conn);
            
            SessionFactoryManager.BuildSchema(GetEnvironment(sessionState), session);
            CurrentSessionContext.Bind(session);

            UserServices.ConfigureSystemUser();
        }

        public static void SetEnvironment(string environment, HttpSessionStateBase sessionState)
        {
            if (sessionState == null) return;
            sessionState[EnvironmentSetting] = environment;
        }
    }
}