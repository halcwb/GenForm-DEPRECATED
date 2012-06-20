using System.Data;
using Informedica.DataAccess.Configurations;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess;
using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Library.Services.Users;
using NHibernate;
using NHibernate.Context;
using StructureMap;

namespace Informedica.GenForm.Services
{
    public class DatabaseServices: IDatabaseServices
    {
        private ISessionStateCache _stateCache;

        public ISessionStateCache SessionStateCache
        {
            set { _stateCache = value; }
            get { return _stateCache ?? (_stateCache = ObjectFactory.GetInstance<EmptySessionStateCache>()); }
        }

        public void ConfigureSessionFactory()
        {
            UseSessionFactoryFromApplicationOrSessionCache(SessionStateCache);
        }

        private static void UseSessionFactoryFromApplicationOrSessionCache(ISessionStateCache stateCache)
        {
            if (IsEmptyCache(stateCache))
                ObjectFactory.Configure(
                    x =>
                    x.For<ISessionFactory>().Use(GenFormApplication.GetSessionFactory(stateCache.GetEnvironment())));
            else
            {
                ObjectFactory.Configure(
                    x =>
                    x.For<ISessionFactory>().Use(stateCache.GetSessionFactory));
            }

        }

        private static bool IsEmptyCache(ISessionStateCache stateCache)
        {
            return stateCache is EmptySessionStateCache;
        }

        public void InitDatabase()
        {
            if (IsEmptyCache(SessionStateCache)) return;
            InitializeDatabase(SessionStateCache);
        }

        private static void InitializeDatabase(ISessionStateCache stateCache)
        {
            // Will cache the connection if in memory database
            SetupConfiguration(stateCache);

            // If database config is in memory, connection will be cached in in session state
            var conn = GetConnectionFromSessionState((IConnectionCache)stateCache);
            if (conn == null) return;

            // Connection has been cache so in memory database
            SetupInMemoryDatabase(stateCache, conn);
        }

        private static void SetupInMemoryDatabase(ISessionStateCache stateCache, IDbConnection conn)
        {
            var fact = SessionFactoryManager.GetSessionFactory(stateCache.GetEnvironment());
            stateCache.SetSessionFactory(fact);

            UseSessionFactoryFromApplicationOrSessionCache(stateCache);
            var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession(conn);

            SessionFactoryManager.BuildSchema(stateCache.GetEnvironment(), session);
            CurrentSessionContext.Bind(session);

            UserServices.ConfigureSystemUser();
        }

        private static IDbConnection GetConnectionFromSessionState(IConnectionCache cache)
        {
            return cache.GetConnection();
        }

        private static void SetupConfiguration(ISessionStateCache stateCache)
        {
            var environment = stateCache.GetEnvironment();
            var envConf = ConfigurationManager.Instance.GetConfiguration(environment);
            envConf.GetConnection();
        }
    }
}