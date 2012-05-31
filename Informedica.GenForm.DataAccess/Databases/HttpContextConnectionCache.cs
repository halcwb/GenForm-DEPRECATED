using System;
using System.Data;
using System.Web;
using Informedica.DataAccess.Configurations;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class HttpContextConnectionCache : IConnectionCache
    {
        public const string Connection = "connection";
        private HttpContextBase _context;

        public HttpContextConnectionCache(HttpContextBase context)
        {
            _context = context;
        }

        #region Implementation of IConnectionCache

        public IDbConnection GetConnection()
        {
            if (_context.Session == null) throw new NullReferenceException("Session is null");
            return (IDbConnection)_context.Session[Connection];
        }

        public void SetConnection(IDbConnection connection)
        {
            if (_context.Session != null) _context.Session[Connection] = connection;
        }

        public bool IsEmpty
        {
            get { return GetConnection() == null; }
        }

        #endregion
    }
}
