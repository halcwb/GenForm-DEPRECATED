using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Repositories
{
    public delegate void Update<T>(GenFormDataContext context, T item);
}