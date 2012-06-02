using System.Data;
using NHibernate;
using NHibernate.Context;
using StructureMap;

namespace Informedica.GenForm.Services
{
    public class SessionServices
    {
        public static void OpenSession()
        {
            CurrentSessionContext.Bind(GetSession());
        }

        private static ISession GetSession()
        {
            return ObjectFactory.TryGetInstance<IDbConnection>() == null ? 
                   ObjectFactory.GetInstance<ISessionFactory>().OpenSession() :
                   ObjectFactory.GetInstance<ISessionFactory>().OpenSession(
                   ObjectFactory.GetInstance<IDbConnection>());
        }
    }
}