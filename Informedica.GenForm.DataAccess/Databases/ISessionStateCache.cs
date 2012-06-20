using NHibernate;

namespace Informedica.GenForm.DataAccess.Databases
{
    public interface ISessionStateCache
    {
        ISessionFactory GetSessionFactory();
        string GetEnvironment();
        void SetSessionFactory(ISessionFactory fact);
    }
}