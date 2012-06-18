using NHibernate;

namespace Informedica.GenForm.DataAccess.Databases
{
    public interface ISessionCache
    {
        ISessionFactory GetSessionFactory();
        string GetEnvironment();
        void SetSessionFactory(ISessionFactory fact);
        bool IsEmpty();
    }
}