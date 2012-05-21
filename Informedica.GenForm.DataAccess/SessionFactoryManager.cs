using FluentNHibernate.Cfg;
using Informedica.DataAccess.Configurations;
using Informedica.DataAccess.Databases;
using Informedica.GenForm.DataAccess.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Informedica.GenForm.DataAccess
{
    public static class SessionFactoryManager
    {
        public const string Test = "TestGenForm";

        static SessionFactoryManager()
        {
            ConfigurationManager.Instance.AddInMemorySqLiteEnvironment<SubstanceMap>(Test);
        }

        public static ISessionFactory GetSessionFactory()
        {
            return GetSessionFactory(Test);
        }

        public static void BuildSchema(string environment, ISession session)
        {
            GetEnvironmentConfiguration(environment).BuildSchema(session);
        }

        public static ISessionFactory GetSessionFactory(string environment)
        {
            return GetEnvironmentConfiguration(environment).GetSessionFactory();
        }

        private static IEnvironmentConfiguration GetEnvironmentConfiguration(string name)
        {
            if (name == Test) return new EnvironmentConfiguration(name, GetConfig(), GetDbConfig() );
            return ConfigurationManager.Instance.GetConfiguration(name);
        }

        private static IDatabaseConfig GetDbConfig()
        {
            return new SqlLiteConfig();
        }

        private static Configuration GetConfig()
        {
            var config = Fluently.Configure()
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<SubstanceMap>())
                .CurrentSessionContext<ThreadStaticSessionContext>()
                .ExposeConfiguration(x => x.SetProperty("connection.release_mode", "on_close"));
            return config.Database(GetDbConfig().Configurer).BuildConfiguration();
        }
    }
}
