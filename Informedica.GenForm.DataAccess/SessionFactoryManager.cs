using Informedica.DataAccess.Configurations;
using Informedica.GenForm.DataAccess.Mappings;
using NHibernate;

namespace Informedica.GenForm.DataAccess
{
    public static class SessionFactoryManager
    {
        private const string Test = "Test";

        static SessionFactoryManager()
        {
            ConfigurationManager.Instance.AddInMemorySqLiteEnvironment<SubstanceMap>(Test);
        }

        public static ISessionFactory GetSessionFactory()
        {
            return GetSessionFactory("Test");
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
            return ConfigurationManager.Instance.GetConfiguration(name);
        }
    }
}
