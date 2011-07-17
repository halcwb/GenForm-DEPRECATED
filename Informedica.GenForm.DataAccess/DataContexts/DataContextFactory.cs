using Informedica.GenForm.DataAccess.Databases;
using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.DataContexts
{
    public class DataContextFactory
    {
        public static GenFormDataContext CreateGenFormDataContext()
        {
            return new GenFormDataContext(DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GenForm));
        }
    }
}
