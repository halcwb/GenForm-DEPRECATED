using System.Data;
using System.Web;
using Informedica.DataAccess.Configurations;
using NHibernate;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class HttpSessionStateCache : IConnectionCache, ISessionStateCache
    {
        public const string SessionFactorySetting = "sessionfactory";
        public const string EnvironmentSetting = "environment";
        public const string ConnectionSetting = "connection";

        private readonly HttpSessionStateBase _sessionState;

        public HttpSessionStateCache(HttpSessionStateBase sessionState)
        {
            _sessionState = sessionState;
        }

        #region Implementation of IConnectionCache

        public IDbConnection GetConnection()
        {
            return (IDbConnection)_sessionState[ConnectionSetting];
        }

        public void SetConnection(IDbConnection connection)
        {
            _sessionState[ConnectionSetting] = connection;
        }

        public bool HasNoConnection
        {
            get { return GetConnection() == null; }
        }

        public void Clear()
        {
            _sessionState.Remove(ConnectionSetting);
        }

        #endregion

        #region Implementation of ISessionCache

        public ISessionFactory GetSessionFactory()
        {
            return (ISessionFactory)_sessionState[SessionFactorySetting];
        }

        public string GetEnvironment()
        {
            return (string)_sessionState[EnvironmentSetting];
        }

        public void SetSessionFactory(ISessionFactory fact)
        {
            _sessionState[SessionFactorySetting] = fact;
        }

        #endregion
    }
}
