using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public delegate void Insert<T>(GenFormDataContext context, T item);
}