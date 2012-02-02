using Informedica.GenForm.DataAccess.Mappings;
using Informedica.DataAccess;
using Informedica.GenForm.Settings;
using NHibernate;

namespace Informedica.GenForm.DataAccess
{
    public static class SessionFactoryManager
    {
        private static Informedica.DataAccess.Databases.SessionFactoryCreator _creator;

        public static ISessionFactory GetSessionFactory()
        {
            return GetSessionFactory("Test");
        }

        public static void BuildSchema(ISession session)
        {
            _creator.BuildSchema(session);
        }

        public static ISessionFactory GetSessionFactory(string environment)
        {
            var connectionString = SettingsManager.Instance.GetConnectionString(environment);
            if (_creator == null)
            {
                _creator = Informedica.DataAccess.Databases.SessionFactoryCreator.CreatSqLiteFactory<SubstanceMap>(connectionString);
            }
            return _creator.CreateSessionFactory();
        }
    }
}
