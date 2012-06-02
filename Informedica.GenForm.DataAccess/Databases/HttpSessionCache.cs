using System.Data;
using System.Web;
using Informedica.DataAccess.Configurations;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class HttpSessionCache : IConnectionCache
    {
        public const string Connection = "connection";
        private HttpSessionStateBase _session;

        public HttpSessionCache(HttpSessionStateBase session)
        {
            _session = session;
        }

        #region Implementation of IConnectionCache

        public IDbConnection GetConnection()
        {
            return (IDbConnection)_session[Connection];
        }

        public void SetConnection(IDbConnection connection)
        {
            _session[Connection] = connection;
        }

        public bool HasNoConnection
        {
            get { return GetConnection() == null; }
        }

        public void Clear()
        {
            _session.Remove(Connection);
        }

        #endregion
    }
}
