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
        private ISessionCache _cache;

        public ISessionCache SessionCache
        {
            set { _cache = value; }
            get { return _cache ?? (_cache = ObjectFactory.GetInstance<EmptySessionCache>()); }
        }

        public void ConfigureSessionFactory()
        {
            UseSessionFactoryFromApplicationOrSessionCache(SessionCache);
        }

        private static void UseSessionFactoryFromApplicationOrSessionCache(ISessionCache cache)
        {
            if (cache.IsEmpty())
                ObjectFactory.Configure(
                    x =>
                    x.For<ISessionFactory>().Use(GenFormApplication.GetSessionFactory(cache.GetEnvironment())));
            else
            {
                ObjectFactory.Configure(
                    x =>
                    x.For<ISessionFactory>().Use(cache.GetSessionFactory));
            }

        }

        public void InitDatabase()
        {
            if (SessionCache.IsEmpty()) return;
            InitializeDatabase(SessionCache);
        }

        private static void InitializeDatabase(ISessionCache cache)
        {
            // Will cache the connection if in memory database
            SetupConfiguration(cache);

            // If database config is in memory, connection will be cached in in session state
            var conn = GetConnectionFromSessionState((IConnectionCache)cache);
            if (conn == null) return;

            // Connection has been cache so in memory database
            SetupInMemoryDatabase(cache, conn);
        }

        private static void SetupInMemoryDatabase(ISessionCache cache, IDbConnection conn)
        {
            var fact = SessionFactoryManager.GetSessionFactory(cache.GetEnvironment());
            cache.SetSessionFactory(fact);

            UseSessionFactoryFromApplicationOrSessionCache(cache);
            var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession(conn);

            SessionFactoryManager.BuildSchema(cache.GetEnvironment(), session);
            CurrentSessionContext.Bind(session);

            UserServices.ConfigureSystemUser();
        }

        private static IDbConnection GetConnectionFromSessionState(IConnectionCache cache)
        {
            return cache.GetConnection();
        }

        private static void SetupConfiguration(ISessionCache cache)
        {
            var environment = cache.GetEnvironment();
            var envConf = ConfigurationManager.Instance.GetConfiguration(environment);
            envConf.GetConnection();
        }
    }
}