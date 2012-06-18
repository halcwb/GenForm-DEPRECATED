using Informedica.GenForm.DataAccess.Databases;

namespace Informedica.GenForm.Services
{
    public interface IDatabaseServices
    {
        void ConfigureSessionFactory();
        void InitDatabase();
        ISessionCache SessionCache { get; set; }
    }
}