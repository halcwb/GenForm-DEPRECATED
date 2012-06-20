using NHibernate;

namespace Informedica.GenForm.DataAccess.Databases
{
    public class EmptySessionStateCache : ISessionStateCache
    {
        #region Implementation of ISessionCache

        public ISessionFactory GetSessionFactory()
        {
            throw new System.NotImplementedException();
        }

        public string GetEnvironment()
        {
            throw new System.NotImplementedException();
        }

        public void SetSessionFactory(ISessionFactory fact)
        {
            throw new System.NotImplementedException();
        }

        public bool IsEmpty()
        {
            return true;
        }

        #endregion
    }
}