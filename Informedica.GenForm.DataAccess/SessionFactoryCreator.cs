using Informedica.GenForm.DataAccess.Mappings;
using NHibernate;

namespace Informedica.GenForm.DataAccess
{
    public static class SessionFactoryCreator
    {
        private static Informedica.DataAccess.Databases.SessionFactoryCreator _creator;

        public static ISessionFactory CreateSessionFactory()
        {
            return CreateSessionFactory("GenFormTest");
        }

        public static void BuildSchema(ISession session)
        {
            _creator.BuildSchema(session);
        }

        public static ISessionFactory CreateSessionFactory(string environment)
        {
            if (_creator == null) _creator =
                Informedica.DataAccess.Databases.SessionFactoryCreator.CreateInMemorySqlLiteFactoryCreator<SubstanceMap>();
            return _creator.CreateSessionFactory();
        }
    }
}
